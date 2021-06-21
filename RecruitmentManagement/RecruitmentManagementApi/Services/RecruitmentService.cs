using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        BaseService<Recruitment, RecruitmentResponse>,
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

        protected override async Task<Recruitment> UpdateEntity(IUpdateableRequest entity)
        {
            var repository = (IRecruitmentRepository)Repository;

            var currentStatus = await repository
                .GetStatus(entity.Id)
                .ConfigureAwait(false);

           var updatedRecruitment = await repository.Update(Mapper.Map<Recruitment>(entity)).ConfigureAwait(false);

            if (currentStatus != updatedRecruitment.Status)
            {
                await recruitmentUpdateHistoryRepository.Create(
                    new()
                    {
                        RecruitmentId = updatedRecruitment.RecruitmentId,
                        Date = DateTime.Now,
                        Status = updatedRecruitment.Status,
                        Note = updatedRecruitment.Note,
                    }
                ).ConfigureAwait(false);
            }
            else
            {
                await recruitmentUpdateHistoryRepository.UpdateLastHistoryNote(
                    updatedRecruitment.RecruitmentId,
                    updatedRecruitment.Note
                ).ConfigureAwait(false);
            }

            return updatedRecruitment;
        }
    }
}