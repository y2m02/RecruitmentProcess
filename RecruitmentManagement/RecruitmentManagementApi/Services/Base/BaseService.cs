using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RecruitmentManagementApi.Models.Enums;
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

        public ResponseType GetAll()
        {
            return HandleErrors(
                () => Success(mapper.Map<IEnumerable<TViewModel>>(Repository.GetAll()))
            );
        }

        public ResponseType Upsert(BaseResponse entity)
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

        public ResponseType Delete(BaseResponse entity)
        {
            return HandleErrors(
                () =>
                {
                    Repository.Delete(mapper.Map<TModel>(entity));

                    return Success(true);
                }
            );
        }

        protected static ResponseType HandleErrors(Func<ResponseType> executor)
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

        protected ResponseType HandleErrors<T>(
            Func<T, ResponseType> executor,
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

        protected static ResponseType Success<T>(T model)
        {
            return new Success<T>(model);
        }
    }
}
