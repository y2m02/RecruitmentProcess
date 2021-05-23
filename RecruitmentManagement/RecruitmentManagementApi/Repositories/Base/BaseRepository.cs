using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;

namespace RecruitmentManagementApi.Repositories.Base
{
    public abstract class BaseRepository<TModel>
    {
        protected BaseRepository(RecruitmentManagementContext context)
        {
            Context = context;
        }

        protected RecruitmentManagementContext Context { get; }

        protected async Task Add(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Added;

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }

        protected async Task Modify(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }

        protected async Task Remove(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }

        protected void AddPropertiesToModify(TModel entity, List<string> properties)
        {
            properties.ForEach(
                propertyName =>
                {
                    Context.Entry(entity).Property(propertyName).IsModified = true;
                }
            );
        }

        protected async Task Save()
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task SaveChangesAndDetach(TModel entity)
        {
            await Save();

            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
