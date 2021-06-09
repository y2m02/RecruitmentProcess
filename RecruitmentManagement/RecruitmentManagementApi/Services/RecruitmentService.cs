using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Repositories.Base;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IRecruitmentService :
        IBaseService,
        ICanUpdateService
    {
        Task<Result> GetHistoryById(int id);
    }

    public class RecruitmentService :
        BaseService<Recruitment>,
        IRecruitmentService

    {
        private readonly IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository;
        private readonly IRecruitmentRepository repository;

        public RecruitmentService(
            IMapper mapper,
            IRecruitmentRepository repository,
            IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository
        ) : base(mapper)
        {
            this.repository = repository;
            this.recruitmentUpdateHistoryRepository = recruitmentUpdateHistoryRepository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<RecruitmentResponse>(repository);
        }

        public Task<Result> GetHistoryById(int id)
        {
            return HandleErrors(
                async () =>
                {
                    return new Result(
                        response: Mapper.Map<List<RecruitmentUpdateHistoryResponse>>(
                            await recruitmentUpdateHistoryRepository.GetByRecruitmentId(id).ConfigureAwait(false)
                        )
                    );
                }
            );
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Update);
        }

        protected override async Task<string> UpdateEntity(
            ICanUpdateRepository<Recruitment> repository,
            IRequest entity
        )
        {
            var recruitmentToUpdate = Mapper.Map<Recruitment>(entity);

            var currentStatus = await this.repository
                .GetStatus(recruitmentToUpdate.RecruitmentId)
                .ConfigureAwait(false);

            await repository.Update(recruitmentToUpdate).ConfigureAwait(false);

            if (currentStatus != recruitmentToUpdate.Status)
            {
                await recruitmentUpdateHistoryRepository.Create(
                    new()
                    {
                        RecruitmentId = recruitmentToUpdate.RecruitmentId,
                        Date = DateTime.Now,
                        Status = recruitmentToUpdate.Status,
                        Note = recruitmentToUpdate.Note,
                    }
                ).ConfigureAwait(false);
            }
            else
            {
                await recruitmentUpdateHistoryRepository.UpdateLastHistoryNote(
                    recruitmentToUpdate.RecruitmentId,
                    recruitmentToUpdate.Note
                ).ConfigureAwait(false);
            }

            return ConsumerMessages.Updated;
        }
    }
}