using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;

namespace RecruitmentManagementApi.Repositories.Base
{
    public abstract class BaseRepository<TModel> where TModel : class
    {
        protected BaseRepository(RecruitmentManagementContext context)
        {
            Context = context;
        }

        protected RecruitmentManagementContext Context { get; }

        public async Task Create(TModel entity)
        {
            await Context.Set<TModel>().AddAsync(entity).ConfigureAwait(false);

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }

        protected Task Modify(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            return SaveChangesAndDetach(entity);
        }

        protected Task Modify(TModel entity, List<string> properties)
        {
            Context.Set<TModel>().Attach(entity);

            AddPropertiesToModify(entity, properties);

            return SaveChangesAndDetach(entity);
        }

        protected Task Remove(TModel entity)
        {
            Context.Set<TModel>().Remove(entity);

            return SaveChangesAndDetach(entity);
        }

        protected Task Remove(IEnumerable<TModel> entities)
        {
            Context.Set<TModel>().RemoveRange(entities);

            return Save();
        }

        private void AddPropertiesToModify(TModel entity, List<string> properties)
        {
            properties.ForEach(
                propertyName =>
                {
                    Context.Entry(entity).Property(propertyName).IsModified = true;
                }
            );
        }

        protected Task Save()
        {
            return Context.SaveChangesAsync();
        }

        private async Task SaveChangesAndDetach(TModel entity)
        {
            await Save().ConfigureAwait(false);

            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
