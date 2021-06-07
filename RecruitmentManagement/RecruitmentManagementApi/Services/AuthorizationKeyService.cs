using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IAuthorizationKeyService : IBaseService
    {
        Task<bool> Exists(string key);
        Task<Result> Get(string key);
    }

    public class AuthorizationKeyService :
        BaseService<AuthorizationKey>,
        IAuthorizationKeyService
    {
        public AuthorizationKeyService(
            IMapper mapper,
            IAuthorizationKeyRepository repository
        ) : base(
            mapper,
            repository
        ) { }

        public Task<bool> Exists(string key)
        {
            return ((IAuthorizationKeyRepository)Repository).Exists(key);
        }

        public Task<Result> Get(string key)
        {
            return HandleErrors(
                async () =>
                {
                    return new Result(
                        response: Mapper.Map<AuthorizationKeyResponse>(
                            await ((IAuthorizationKeyRepository)Repository).Get(key).ConfigureAwait(false)
                        )
                    );
                }
            );
        }
    }
}