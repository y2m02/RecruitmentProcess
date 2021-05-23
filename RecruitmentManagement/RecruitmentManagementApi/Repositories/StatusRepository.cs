using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task BatchCreate(IEnumerable<Status> statuses);
        Task BatchUpdate(IEnumerable<Status> statuses);
        Task BatchDelete(IEnumerable<Status> statuses);
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

            await Save().ConfigureAwait(false);
        }

        public Task Update(Status entity)
        {
            return Modify(entity);
        }

        public async Task BatchUpdate(IEnumerable<Status> statuses)
        {
            statuses.ForAll(entity =>
            {
                Context.Attach(entity);

                AddPropertiesToModify(entity, new List<string>
                {
                    nameof(entity.Description)
                });
            });

            await Save().ConfigureAwait(false);
        }

        public Task Delete(Status entity)
        {
            return Remove(entity);
        }

        public async Task BatchDelete(IEnumerable<Status> statuses)
        {
            statuses.ForAll(entity =>
            {
                Context.Attach(entity);

                Context.Entry(entity).State = EntityState.Deleted;
            });

            await Save().ConfigureAwait(false);
        }
    }
}