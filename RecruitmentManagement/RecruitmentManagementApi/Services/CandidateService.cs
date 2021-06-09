using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
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

        public CandidateService(
            IMapper mapper,
            ICandidateRepository repository,
            IRecruitmentRepository recruitmentRepository,
            IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository
        ) : base(mapper)
        {
            Repository = repository;

            this.recruitmentRepository = recruitmentRepository;
            this.recruitmentUpdateHistoryRepository = recruitmentUpdateHistoryRepository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<CandidateResponse>();
        }

        protected override async Task<string> CreateEntity(IRequest entity)
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

            await Repository.Create(candidate).ConfigureAwait(false);

            return ConsumerMessages.Created;
        }

        protected override async Task DeleteEntity(int id)
        {
            await recruitmentUpdateHistoryRepository.BatchDelete(
                await recruitmentUpdateHistoryRepository.GetAllByRecruitmentId(id).ConfigureAwait(false)
            );

            await recruitmentRepository.Delete(id).ConfigureAwait(false);

            await base.DeleteEntity(id).ConfigureAwait(false);
        }
    }
}