using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Extensions;
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

        public Task<Result> Delete(IRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    await Repository.Delete(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1,"eliminado/s")
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

                    var successMessage = string.Empty;
                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await Repository.Create(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

                            successMessage = ConsumerMessages.SuccessResponse.Format(1, "creado/s");
                            break;

                        case UpsertActionType.Update:
                            await Repository.Update(Mapper.Map<TModel>(entity)).ConfigureAwait(false);
                            
                            successMessage = ConsumerMessages.SuccessResponse.Format(1, "actualizado/s");
                            break;
                    }

                    return new Result(response: successMessage);
                }
            );
        }
    }
}