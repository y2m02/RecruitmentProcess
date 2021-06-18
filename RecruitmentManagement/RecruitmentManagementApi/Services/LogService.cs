using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface ILogService : IBaseService { }

    public class LogService :
        BaseService<Log>,
        ILogService
    {
        public LogService(
            IMapper mapper,
            ILogRepository repository,
            ILogRepository logRepository
        ) : base(mapper, logRepository)
        {
            Repository = repository;
        }

        public Task<Result> GetAll() => GetAll<LogResponse>();
    }
}