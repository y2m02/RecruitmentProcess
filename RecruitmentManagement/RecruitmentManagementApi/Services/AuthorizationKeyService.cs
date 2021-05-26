using System.Threading.Tasks;
using RecruitmentManagementApi.Repositories;

namespace RecruitmentManagementApi.Services
{
    public interface IAuthorizationKeyService
    {
        Task<bool> Exists(string key);
    }

    public class AuthorizationKeyService : IAuthorizationKeyService
    {
        private readonly IAuthorizationKeyRepository authorizationKeyRepository;

        public AuthorizationKeyService(
            IAuthorizationKeyRepository authorizationKeyRepository
        )
        {
            this.authorizationKeyRepository = authorizationKeyRepository;
        }

        public Task<bool> Exists(string key)
        {
            return authorizationKeyRepository.Exists(key);
        }
    }
}
