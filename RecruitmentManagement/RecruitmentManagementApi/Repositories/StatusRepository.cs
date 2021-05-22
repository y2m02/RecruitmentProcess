﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status> { }

    public class StatusRepository :
        BaseRepository<Status>,
        IStatusRepository
    {
        public StatusRepository(RecruitmentManagementContext context) : base(context) { }

        public IEnumerable<Status> GetAll()
        {
            return Context.Statuses
                .OrderBy(w => w.Description)
                .AsNoTracking();
        }

        public void Create(Status entity)
        {
            Add(entity);
        }

        public void Update(Status entity)
        {
            Modify(entity);
        }

        public void Delete(Status entity)
        {
            Remove(entity);
        }
    }
}
