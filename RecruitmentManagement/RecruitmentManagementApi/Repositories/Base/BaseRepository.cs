using System.Collections.Generic;
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

        protected void Add(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Added;

            SaveChangesAndDetach(entity);
        }

        protected void Modify(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            SaveChangesAndDetach(entity);
        }

        protected void Remove(TModel entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            SaveChangesAndDetach(entity);
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

        private void SaveChangesAndDetach(TModel entity)
        {
            Context.SaveChanges();

            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
