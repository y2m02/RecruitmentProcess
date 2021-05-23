using System;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IRecruitmentService : IBaseService { }

    public class RecruitmentService :
        BaseService<Recruitment>,
        IRecruitmentService

    {
        private readonly IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository;

        public RecruitmentService(
            IMapper mapper,
            IRecruitmentRepository recruitmentRepository,
            IRecruitmentUpdateHistoryRepository recruitmentUpdateHistoryRepository
        ) : base(mapper, recruitmentRepository)
        {
            this.recruitmentUpdateHistoryRepository = recruitmentUpdateHistoryRepository;
        }

        protected override async Task<string> UpdateEntity(IRequest entity)
        {
            var recruitment = Mapper.Map<Recruitment>(entity);

            await Repository.Update(recruitment).ConfigureAwait(false);

            await recruitmentUpdateHistoryRepository.Create(
                new()
                {
                    RecruitmentId = recruitment.RecruitmentId,
                    Date = DateTime.Now,
                    Status = recruitment.Status,
                    Note = recruitment.Note,
                }
            ).ConfigureAwait(false);

            return ConsumerMessages.Updated;
        }
    }
}