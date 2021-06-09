using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public abstract class BaseService<TModel>
    {
        protected readonly IMapper Mapper;

        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected Task<Result> GetAll<TResponse>(IBaseRepository<TModel> repository)
            where TResponse : BaseResponse
        {
            return HandleErrors(
                async () =>
                {
                    return new Result(
                        response: Mapper.Map<List<TResponse>>(
                            await repository.GetAll().ConfigureAwait(false)
                        )
                    );
                }
            );
        }

        protected Task<Result> Delete(
            ICanDeleteRepository<TModel> repository,
            IRequest entity
        )
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await repository.Delete(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, ConsumerMessages.Deleted)
                    );
                }
            );
        }

        protected async Task<Result> HandleErrors(Func<Task<Result>> executor)
        {
            try
            {
                return await executor();
            }
            catch (Exception ex)
            {
                return new Result(errorMessage: ex.Message);
            }
        }

        protected Result HandleErrors<T>(
            Func<T, Result> executor,
            T request
        )
        {
            try
            {
                return executor(request);
            }
            catch (Exception ex)
            {
                return new Result(errorMessage: ex.Message);
            }
        }

        protected Task<Result> Upsert(
            IBaseRepository<TModel> repository,
            IRequest entity,
            UpsertActionType actionType
        )
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    var action = actionType == UpsertActionType.Create
                        ? await CreateEntity(repository, entity).ConfigureAwait(false)
                        : await UpdateEntity((ICanUpdateRepository<TModel>)repository, entity).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, action)
                    );
                }
            );
        }

        protected virtual async Task<string> CreateEntity(
            IBaseRepository<TModel> repository,
            IRequest entity
        )
        {
            await repository.Create(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Created;
        }

        protected virtual async Task<string> UpdateEntity(
            ICanUpdateRepository<TModel> repository,
            IRequest entity
        )
        {
            await repository.Update(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Updated;
        }
    }
}
