using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IAuthorizationKeyService : 
        IBaseService,
        ICanDeleteService
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
        ) : base(mapper)
        {
            Repository = repository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<AuthorizationKeyResponse>();
        }

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