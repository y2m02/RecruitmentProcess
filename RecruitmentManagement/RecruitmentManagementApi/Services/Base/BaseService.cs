using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public abstract class BaseService<TModel>
    {
        private readonly ILogRepository logRepository;
        protected readonly IMapper Mapper;

        protected BaseService(
            IMapper mapper,
            ILogRepository logRepository
        )
        {
            Mapper = mapper;
            this.logRepository = logRepository;
        }

        protected IBaseRepository<TModel> Repository { get; set; }

        protected Task<Result> GetAll<TResponse>() where TResponse : BaseResponse
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

        protected virtual async Task<string> CreateEntity(IRequest entity)
        {
            await Repository.Create(Mapper.Map<TModel>(entity)).ConfigureAwait(false);

            return ConsumerMessages.Created;
        }

        protected virtual async Task<string> UpdateEntity(IRequest entity)
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

        protected Result HandleErrors<T>(Func<T, Result> executor, T request)
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

                    var action = actionType == UpsertActionType.Create
                        ? await CreateEntity(entity).ConfigureAwait(false)
                        : await UpdateEntity(entity).ConfigureAwait(false);

                    var endpoint = string.Empty;
                    var affectedEntity = 0;

                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await CreateEntity(entity).ConfigureAwait(false);

                            endpoint = nameof(Create);
                            break;

                        case UpsertActionType.Update:
                            await UpdateEntity(entity).ConfigureAwait(false);

                            affectedEntity = ((IUpdateableRequest)entity).Id;
                            endpoint = nameof(Update);
                            break;
                    }

                    await logRepository.Create(
                        CreateLog("Test", entity.GetType().Name, endpoint, affectedEntity)
                    ).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(1, 1, action)
                    );
                }
            );
        }

        private static Log CreateLog(
            string apiKey,
            string entityType,
            string endPoint,
            int affectedEntity
        )
        {
            return new()
            {
                RunAt = DateTime.Now,
                ApiKey = apiKey,
                Endpoint = endPoint,
                Api = entityType switch
                {
                    _ when entityType.Contains("AuthorizationKey") => Api.AuthorizationKey,
                    _ when entityType.Contains("Candidate") => Api.Candidate,
                    _ when entityType.Contains("Recruitment") => Api.Recruitment,
                },
                AffectedEntity = affectedEntity,
            };
        }
    }
}