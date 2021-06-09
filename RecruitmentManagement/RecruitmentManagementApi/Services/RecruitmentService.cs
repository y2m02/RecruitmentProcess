using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
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

        public RecruitmentService(
            IMapper mapper,
            IRecruitmentRepository repository,
            IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository
        ) : base(mapper)
        {
            Repository = repository;

            this.recruitmentUpdateHistoryRepository = recruitmentUpdateHistoryRepository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<RecruitmentResponse>();
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

        protected override async Task<string> UpdateEntity(IRequest entity)
        {
            var repository = (IRecruitmentRepository)Repository;

            var recruitmentToUpdate = Mapper.Map<Recruitment>(entity);

            var currentStatus = await repository
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