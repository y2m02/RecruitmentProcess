using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
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

        private Task<Result> BatchUpsert(IEnumerable<StatusRequest> statuses, UpsertActionType actionType)
        {
            return HandleErrors(
                async () =>
                {
                    var statusesToSubmit = new List<StatusRequest>();
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
                                )
                                .ConfigureAwait(false);

                            break;

                        case UpsertActionType.Update:
                            //await Repository.Update(Mapper.Map<TModel>(statusToSubmit)).ConfigureAwait(false);
                            break;
                    }

                    return new Result(
                        response: new
                        {
                            success = statusesToSubmit.Select(x => x.Description),
                        },
                        validationErrors: statusesToValidate
                    );
                }
            );
        }
    }
}