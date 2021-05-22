using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
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

        public Task<BaseResponse> GetAll<TResponse>()
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

        public Task<BaseResponse> Upsert(BaseRequest entity)
        {
            return HandleErrors(
               async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Validation(validations);
                    }

                    switch (entity.UpsertActionType)
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

        public Task<BaseResponse> Delete(BaseResponse entity)
        {
            return HandleErrors(
                async () =>
                {
                     await Repository.Delete(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

                    return Success(true);
                }
            );
        }

        protected async Task<BaseResponse> HandleErrors(Func<Task<BaseResponse>> executor)
        {
            try
            {
                return await executor();
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }
        }

        protected BaseResponse HandleErrors<T>(
            Func<T, BaseResponse> executor,
            T request
        )
        {
            try
            {
                return executor(request);
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }
        }

        protected BaseResponse Success<T>(T model)
        {
            return new Success<T>(model);
        }
    }
}