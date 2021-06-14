using System.Collections.Generic;
using System.Threading.Tasks;
using HelpersLibrary.Extensions;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IAuthorizationKeyRepository :
        IBaseRepository<AuthorizationKey>,
        ICanDeleteRepository
    {
        Task<bool> Exists(string key);
        Task<AuthorizationKey> Get(string key);
        Task Activate(int id);
        Task Deactivate(int id);
        Task UpdatePermissions(int id, IEnumerable<Permission> permissions);
    }

    public class AuthorizationKeyRepository :
        BaseRepository<AuthorizationKey>,
        IAuthorizationKeyRepository
    {
        public AuthorizationKeyRepository(
            RecruitmentManagementContext context
        ) : base(context) { }

        public Task<List<AuthorizationKey>> GetAll()
        {
            return Context.AuthorizationKeys.AsNoTracking().ToListAsync();
        }

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

        public Task Activate(int id) => UpdateStatus(id, isActive: true);

        public Task Deactivate(int id) => UpdateStatus(id, isActive: false);

        public Task UpdatePermissions(int id, IEnumerable<Permission> permissions)
        {
            var authorizationKey = new AuthorizationKey
            {
                AuthorizationKeyId = id, 
                Permissions = permissions.Join("-"),
            };

            return Modify(
                authorizationKey,
                new() { nameof(authorizationKey.Permissions) }
            );
        }

        public Task Delete(int id) => Remove(new AuthorizationKey { AuthorizationKeyId = id });

        private Task UpdateStatus(int id, bool isActive)
        {
            var authorizationKey = new AuthorizationKey
            {
                AuthorizationKeyId = id, 
                IsActive = isActive,
            };

            return Modify(
                authorizationKey,
                new() { nameof(authorizationKey.IsActive) }
            );
        }
    }
}