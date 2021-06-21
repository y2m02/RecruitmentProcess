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

        public async Task<TModel> Create(TModel entity)
        {
            await Context.Set<TModel>().AddAsync(entity).ConfigureAwait(false);

            await SaveChangesAndDetach(entity).ConfigureAwait(false);

            return entity;
        }

        protected Task Modify(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            return SaveChangesAndDetach(entity);
        }

        protected async Task<TModel> Modify(int id, TModel entity, List<string> properties)
        {
            Context.Set<TModel>().Attach(entity);

            AddPropertiesToModify(entity, properties);

            await SaveChangesAndDetach(entity).ConfigureAwait(false);

            return await GetById(id).ConfigureAwait(false);
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

        protected Task Save() => Context.SaveChangesAsync();

        protected virtual Task<TModel> GetById(int id)
        {
            return Context.Set<TModel>().FindAsync(id).AsTask();
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

        private async Task SaveChangesAndDetach(TModel entity)
        {
            await Save().ConfigureAwait(false);

            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
