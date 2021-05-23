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
using RecruitmentManagementApi.Repositories.Base;
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
        ) : base(mapper)
        {
            this.statusRepository = statusRepository;
        }

        protected override IBaseRepository<Status> Repository => statusRepository;

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

                    await ((IStatusRepository)Repository).BatchDelete(
                        Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                    ).ConfigureAwait(false);

                    return new Result(
                        response: ConsumerMessages.SuccessResponse.Format(
                            statusesToSubmit.Count,
                            "eliminado/s"
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

                    var successMessage = string.Empty;

                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await ((IStatusRepository)Repository).BatchCreate(
                                    Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                                )
                                .ConfigureAwait(false);

                            successMessage = ConsumerMessages.SuccessResponse.Format(
                                statusesToSubmit.Count,
                                "creado/s"
                            );

                            break;

                        case UpsertActionType.Update:
                            await ((IStatusRepository)Repository).BatchUpdate(
                                    Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                                )
                                .ConfigureAwait(false);

                            successMessage = ConsumerMessages.SuccessResponse.Format(
                                statusesToSubmit.Count,
                                "actualizado/s"
                            );

                            break;
                    }

                    return new Result(
                        response: successMessage,
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