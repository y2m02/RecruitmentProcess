using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Request.Candidates;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Repositories.Base;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface ICandidateService :
        IBaseService,
        ICanUpdateService,
        ICanDeleteService { }

    public class CandidateService :
        BaseService<Candidate>,
        ICandidateService
    {
        private readonly IRecruitmentRepository recruitmentRepository;
        private readonly IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository;
        private readonly ICandidateRepository repository;

        public CandidateService(
            IMapper mapper,
            ICandidateRepository repository,
            IRecruitmentRepository recruitmentRepository,
            IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository
        ) : base(mapper)
        {
            this.repository = repository;
            this.recruitmentRepository = recruitmentRepository;
            this.recruitmentUpdateHistoryRepository = recruitmentUpdateHistoryRepository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<CandidateResponse>(repository);
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Update);
        }

        public Task<Result> Delete(IRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    var candidate = entity as DeleteCandidateRequest;

                    await recruitmentUpdateHistoryRepository.BatchDelete(
                        await recruitmentUpdateHistoryRepository.GetAllByRecruitmentId(candidate.Id)
                            .ConfigureAwait(false)
                    );

                    await recruitmentRepository
                        .Delete(
                            new Recruitment
                            {
                                RecruitmentId = candidate.Id,
                            }
                        )
                        .ConfigureAwait(false);

                    await repository.Delete(Mapper.Map<Candidate>(entity)).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, ConsumerMessages.Deleted)
                    );
                }
            );
        }

        protected override async Task<string> CreateEntity(
            IBaseRepository<Candidate> repository,
            IRequest entity
        )
        {
            var candidate = Mapper
                .Map<Candidate>(entity)
                .Tap(
                    x => x.Recruitment = new()
                    {
                        Date = x.Date,
                        Status = RecruitmentStatus.Pending,
                        RecruitmentUpdateHistories = new List<RecruitmentUpdateHistory>
                        {
                            new()
                            {
                                Date = x.Date, Status = RecruitmentStatus.Pending,
                            },
                        },
                    }
                );

            await repository.Create(candidate).ConfigureAwait(false);

            return ConsumerMessages.Created;
        }
    }
}