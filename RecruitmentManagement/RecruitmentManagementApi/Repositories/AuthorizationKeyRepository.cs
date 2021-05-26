using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IAuthorizationKeyRepository
    {
        Task<bool> Exists(string key);
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
    }
}
