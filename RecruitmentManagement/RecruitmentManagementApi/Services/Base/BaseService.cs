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
        protected readonly IBaseRepository<TModel> Repository;

        protected BaseService(
            IMapper mapper,
            IBaseRepository<TModel> repository
        )
        {
            Mapper = mapper;
            Repository = repository;
        }

        public Task<Result> GetAll<TResponse>() where TResponse : BaseResponse
        {
            return HandleErrors(
                async () =>
                {
                    return new Result(
                        response: Mapper.Map<List<TResponse>>(
                            await Repository.GetAll().ConfigureAwait(false)
                        )
                    );
                }
            );
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IRequest entity)
        {
            return Upsert(entity, UpsertActionType.Update);
        }

        public virtual Task<Result> Delete(IRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await Repository.Delete(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

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

        protected virtual async Task<string> CreateEntity(IRequest entity)
        {
            await Repository.Create(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Created;
        }

        protected virtual async Task<string> UpdateEntity(IRequest entity)
        {
            await Repository.Update(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Updated;
        }

        private Task<Result> Upsert(IRequest entity, UpsertActionType actionType)
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
                        ? await CreateEntity(entity).ConfigureAwait(false)
                        : await UpdateEntity(entity).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, action)
                    );
                }
            );
        }
    }
}