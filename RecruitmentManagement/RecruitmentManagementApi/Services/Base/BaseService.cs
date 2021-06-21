using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public abstract class BaseService<TModel, TResponse>
        where TModel : class
        where TResponse : BaseResponse
    {
        protected readonly IMapper Mapper;

        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IBaseRepository<TModel> Repository { get; set; }

        public Task<Result> GetAll()
        {
            return HandleErrors(
                async () => new Result(
                    response: Mapper.Map<List<TResponse>>(
                        await Repository.GetAll().ConfigureAwait(false)
                    )
                )
            );
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IUpdateableRequest entity)
        {
            return Upsert(entity, UpsertActionType.Update);
        }

        public Task<Result> Delete(DeleteRequest entity)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Result(validationErrors: validations);
                    }

                    await ((ICanDeleteRepository)Repository).Delete(entity.Id).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, ConsumerMessages.Deleted)
                    );
                }
            );
        }

        protected virtual async Task<TModel> CreateEntity(IRequest entity)
        {
            var model = Mapper.Map<TModel>(entity);

            var a  = await Repository.Create(model).ConfigureAwait(false);

            return a;
        }

        protected virtual async Task<string> UpdateEntity(IUpdateableRequest entity)
        {
            var repository = (ICanUpdateRepository<TModel>)Repository;

            await repository.Update(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Updated;
        }

        protected async Task<Result> HandleErrors(Func<Task<Result>> executor)
        {
            try
            {
                return await executor().ConfigureAwait(false);
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

                    var a = await CreateEntity(entity).ConfigureAwait(false);

                    var response = Mapper.Map<TResponse>(
                        a
                    );
                    //var action = actionType == UpsertActionType.Create
                    //    ? await CreateEntity(entity).ConfigureAwait(false)
                    //    : await UpdateEntity(entity).ConfigureAwait(false);

                    return new Result(response);
                }
            );
        }
    }
}
