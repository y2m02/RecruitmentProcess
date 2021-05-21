using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RecruitmentManagementApp.Models.Enums;
using RecruitmentManagementApp.Models.ViewModels.Base;
using RecruitmentManagementApp.Repositories.Base;

namespace RecruitmentManagementApp.Services.Base
{
    public abstract class BaseService<TModel, TViewModel>
    {
        protected readonly IMapper mapper;

        protected BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        protected abstract IBaseRepository<TModel> Repository { get; }

        public BaseReturnViewModel GetAll()
        {
            return HandleErrors(
                () => Success(mapper.Map<IEnumerable<TViewModel>>(Repository.GetAll()))
            );
        }

        public BaseReturnViewModel Upsert(BaseViewModel entity)
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

        public BaseReturnViewModel Delete(BaseViewModel entity)
        {
            return HandleErrors(
                () =>
                {
                    Repository.Delete(mapper.Map<TModel>(entity));

                    return Success(true);
                }
            );
        }

        protected static BaseReturnViewModel HandleErrors(Func<BaseReturnViewModel> executor)
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

        protected BaseReturnViewModel HandleErrors<T>(
            Func<T, BaseReturnViewModel> executor,
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

        protected static BaseReturnViewModel Success<T>(T model)
        {
            return new Success<T>(model);
        }
    }
}
