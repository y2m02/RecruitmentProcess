using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IAuthorizationKeyRepository:IBaseRepository<AuthorizationKey>
    {
        Task<bool> Exists(string key);
        Task<AuthorizationKey> Get(string key);
    }

    public class AuthorizationKeyRepository :
        BaseRepository<AuthorizationKey>,
        IAuthorizationKeyRepository
    {
        public AuthorizationKeyRepository(
            RecruitmentManagementContext context
        ) : base(context) { }

        public Task<bool> Exists(string key)
        {
            return Context
                .AuthorizationKeys
                .AnyAsync(x => x.IsActive && x.Key == key);
        }

        public Task<AuthorizationKey> Get(string key)
        {
            return Context
                .AuthorizationKeys
                .SingleOrDefaultAsync(x => x.IsActive && x.Key == key);
        }

        public Task<List<AuthorizationKey>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Create(AuthorizationKey entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(AuthorizationKey entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(AuthorizationKey entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
