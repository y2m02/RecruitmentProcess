using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public abstract class BaseService<TModel, TViewModel>
    {
        protected readonly IMapper mapper;

        protected BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        protected abstract IBaseRepository<TModel> Repository { get; }

        public BaseResponse GetAll()
        {
            return HandleErrors(
                () => Success(mapper.Map<IEnumerable<TViewModel>>(Repository.GetAll()))
            );
        }

        public BaseResponse Upsert(BaseRequest entity)
        {
            return HandleErrors(
                () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Validation(validations);
                    }

                    switch (entity.UpsertActionType)
                    {
                        case UpsertActionType.Create:
                            Repository.Create(mapper.Map<TModel>(entity));
                            break;

                        case UpsertActionType.Update:
                            Repository.Update(mapper.Map<TModel>(entity));
                            break;
                    }

                    return Success(true);
                }
            );
        }

        public BaseResponse Delete(BaseResponse entity)
        {
            return HandleErrors(
                () =>
                {
                    Repository.Delete(mapper.Map<TModel>(entity));

                    return Success(true);
                }
            );
        }

        protected static BaseResponse HandleErrors(Func<BaseResponse> executor)
        {
            try
            {
                return executor();
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

        protected static BaseResponse Success<T>(T model)
        {
            return new Success<T>(model);
        }
    }
}
