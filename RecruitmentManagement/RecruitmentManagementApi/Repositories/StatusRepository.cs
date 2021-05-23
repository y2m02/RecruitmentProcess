using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task BatchCreate(IEnumerable<Status> statuses);
    }

    public class StatusRepository :
        BaseRepository<Status>,
        IStatusRepository

    {
        public StatusRepository(RecruitmentManagementContext context) : base(context) { }

        public Task<List<Status>> GetAll()
        {
            return Context.Statuses
                .OrderBy(x => x.StatusId)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task Create(Status entity)
        {
            return Add(entity);
        }

        public async Task BatchCreate(IEnumerable<Status> statuses)
        {
            await Context.Statuses.AddRangeAsync(statuses).ConfigureAwait(false);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task Update(Status entity)
        {
            return Modify(entity);
        }

        public Task Delete(Status entity)
        {
            return Remove(entity);
        }
    }
}