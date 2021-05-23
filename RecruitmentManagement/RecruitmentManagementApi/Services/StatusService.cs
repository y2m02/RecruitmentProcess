using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
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

        private Task<Result> BatchUpsert(IEnumerable<IBaseRequest> statuses, UpsertActionType actionType)
        {
            return HandleErrors(
                async () =>
                {
                    var statusesToSubmit = new List<IBaseRequest>();
                    var statusesToValidate = new List<string>();

                    foreach (var status in statuses)
                    {
                        var validations = status.Validate().ToList();

                        if (validations.Any())
                        {
                            statusesToValidate.AddRange(validations);

                            continue;
                        }

                        statusesToSubmit.Add(status);
                    }

                    switch (actionType)
                    {
                        case UpsertActionType.Create:
                            await ((IStatusRepository)Repository).BatchCreate(
                                Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                            ).ConfigureAwait(false);
                            break;

                        case UpsertActionType.Update:
                            await ((IStatusRepository)Repository).BatchUpdate(
                                Mapper.Map<IEnumerable<Status>>(statusesToSubmit)
                            ).ConfigureAwait(false);
                            break;
                    }

                    return new Result(
                        response: statusesToSubmit.Select(_ => "Success"),
                        validationErrors: statusesToValidate
                    );
                }
            );
        }
    }
}