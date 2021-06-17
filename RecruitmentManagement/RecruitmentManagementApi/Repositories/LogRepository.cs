using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface ILogRepository : IBaseRepository<Log> { }

    public class LogRepository :
        BaseRepository<Log>,
        ILogRepository
    {
        public LogRepository(RecruitmentManagementContext context) : base(context) { }

        public Task<List<Log>> GetAll()
        {
            return Context.Logs.ToListAsync();
        }
    }
}