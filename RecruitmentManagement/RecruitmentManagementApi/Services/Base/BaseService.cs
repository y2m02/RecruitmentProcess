using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        protected abstract IBaseRepository<TModel> Repository { get; }

        public Task<BaseResult> GetAll<TResponse>()
        {
            return HandleErrors(
                async () =>
                {
                    return Success(
                        Mapper.Map<List<TResponse>>(
                            await Repository.GetAll().ConfigureAwait(false)
                        )
                    );
                }
            );
        }

        public Task<BaseResult> Create(IBaseRequest entity)
        {
            return Upsert(entity, UpsertActionType.Create);
        }

        public Task<BaseResult> Update(IBaseRequest entity)
        {
            return Upsert(entity, UpsertActionType.Update);
        }

        public Task<BaseResult> Delete(IBaseRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    await Repository.Delete(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

                    return Success(true);
                }
            );
        }

        protected async Task<BaseResult> HandleErrors(Func<Task<BaseResult>> executor)
        {
            try
            {
                return await executor();
            }
            catch (Exception ex)
            {
                return new FailureResult(ex.Message);
            }
        }

        protected BaseResult HandleErrors<T>(
            Func<T, BaseResult> executor,
            T request
        )
        {
            try
            {
                return executor(request);
            }
            catch (Exception ex)
            {
                return new FailureResult(ex.Message);
            }
        }

        protected BaseResult Success<T>(T model)
        {
            return new SuccessResult<T>(model);
        }

        private Task<BaseResult> Upsert(IBaseRequest entity, UpsertActionType actionType)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new ValidationResult(validations);
                    }

                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await Repository.Create(Mapper.Map<TModel>(entity)).ConfigureAwait(false);
                            break;

                        case UpsertActionType.Update:
                            await Repository.Update(Mapper.Map<TModel>(entity)).ConfigureAwait(false);
                            break;
                    }

                    return Success(true);
                }
            );
        }
    }
}