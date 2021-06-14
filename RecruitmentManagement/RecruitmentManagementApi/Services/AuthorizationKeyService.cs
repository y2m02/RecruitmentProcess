using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.AuthorizationKey;
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
        Task<Result> Activate(UpdateAuthorizationKeyStatusRequest entity);
        Task<Result> Deactivate(UpdateAuthorizationKeyStatusRequest entity);
        Task<Result> UpdatePermissions(UpdateAuthorizationKeyPermissionsRequest entity);
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

        public Task<Result> Activate(UpdateAuthorizationKeyStatusRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await ((IAuthorizationKeyRepository)Repository).Activate(entity.Id).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, "activated")
                    );
                }
            );
        }

        public Task<Result> Deactivate(UpdateAuthorizationKeyStatusRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await ((IAuthorizationKeyRepository)Repository).Deactivate(entity.Id).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, "deactivated")
                    );
                }
            );
        }

        public Task<Result> UpdatePermissions(UpdateAuthorizationKeyPermissionsRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await ((IAuthorizationKeyRepository)Repository)
                        .UpdatePermissions(entity.Id, entity.Permissions)
                        .ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, "updated")
                    );
                }
            );
        }
    }
}