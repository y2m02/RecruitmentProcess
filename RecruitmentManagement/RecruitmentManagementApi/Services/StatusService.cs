using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Request.Statuses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface IStatusService : IBaseService
    {
        Task<Result> BatchCreate(IEnumerable<StatusRequest> statuses);
        Task<Result> BatchUpdate(IEnumerable<UpdateStatusRequest> statuses);
        Task<Result> BatchDelete(IEnumerable<DeleteStatusRequest> statuses);
    }

    public class StatusService :
        BaseService<Status>,
        IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(
            IMapper mapper,
            IStatusRepository statusRepository
        ) : base(mapper, statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public Task<Result> BatchCreate(IEnumerable<StatusRequest> statuses)
        {
            return BatchUpsert(statuses, UpsertActionType.Create);
        }

        public Task<Result> BatchUpdate(IEnumerable<UpdateStatusRequest> statuses)
        {
            return BatchUpsert(statuses, UpsertActionType.Update);
        }

        public Task<Result> BatchDelete(IEnumerable<DeleteStatusRequest> statuses)
        {
            return HandleErrors(
                async () =>
                {
                    var (statusesToSubmit, validationErrors) = SplitRequest(statuses);

                    await statusRepository.BatchDelete(
                        Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                    ).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(
                            statusesToSubmit.Count,
                            statuses.Count(),
                            ConsumerMessages.Deleted
                        ),
                        validationErrors: validationErrors
                    );
                }
            );
        }

        private Task<Result> BatchUpsert(IEnumerable<IRequest> statuses, UpsertActionType actionType)
        {
            return HandleErrors(
                async () =>
                {
                    var (statusesToSubmit, validationErrors) = SplitRequest(statuses);

                    var action = string.Empty;

                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await statusRepository.BatchCreate(
                                    Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                                )
                                .ConfigureAwait(false);

                            action = ConsumerMessages.Created;
                            break;

                        case UpsertActionType.Update:
                            await statusRepository.BatchUpdate(
                                    Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                                )
                                .ConfigureAwait(false);

                            action = ConsumerMessages.Updated;
                            break;
                    }

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(
                            statusesToSubmit.Count,
                            statuses.Count(),
                            action
                        ),
                        validationErrors: validationErrors
                    );
                }
            );
        }

        private static (List<IRequest> validated, List<string> validationErrors) SplitRequest(IEnumerable<IRequest> statuses)
        {
            var statusesToSubmit = new List<IRequest>();
            var validationErrors = new List<string>();

            foreach (var status in statuses)
            {
                var validations = status.Validate().ToList();

                if (validations.Any())
                {
                    validationErrors.AddRange(validations);

                    continue;
                }

                statusesToSubmit.Add(status);
            }

            return (statusesToSubmit, validationErrors);
        }
    }
}