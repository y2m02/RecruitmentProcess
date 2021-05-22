using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Repositories.Base;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IStatusService : IBaseService { }

    public class StatusService :
        BaseService<Status>,
        IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(
            IMapper mapper,
            IStatusRepository statusRepository
        ) : base(mapper)
        {
            this.statusRepository = statusRepository;
        }

        protected override IBaseRepository<Status> Repository => statusRepository;
    }
}