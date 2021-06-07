using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
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
        private readonly IAuthorizationKeyRepository repository;

        public AuthorizationKeyService(
            IMapper mapper,
            IAuthorizationKeyRepository repository
        ) : base(mapper)
        {
            this.repository = repository;
        }

        public Task<Result> GetAll()
        {
            return GetAll<AuthorizationKeyResponse>(repository);
        }

        public Task<bool> Exists(string key)
        {
            return repository.Exists(key);
        }

        public Task<Result> Get(string key)
        {
            return HandleErrors(
                async () =>
                {
                    return new Result(
                        response: Mapper.Map<AuthorizationKeyResponse>(
                            await repository.Get(key).ConfigureAwait(false)
                        )
                    );
                }
            );
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IRequest entity)
        {
            return Upsert(repository, entity, UpsertActionType.Update);
        }

        public Task<Result> Delete(IRequest entity)
        {
            throw new System.NotImplementedException();
        }
    }
}