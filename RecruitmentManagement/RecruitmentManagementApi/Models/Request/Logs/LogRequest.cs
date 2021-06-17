using System;
using System.Collections.Generic;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Logs
{
    public class LogRequest : IRequest
    {
        public DateTime? RunAt { get; set; }

        public Api? Api { get; set; }

        public string Endpoint { get; set; }

        public string ApiKey { get; set; }

        public int? AffectedEntity { get; set; }

        public IEnumerable<string> Validate()
        {
            if (!RunAt.HasValue)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(RunAt));
            }

            if (!Api.HasValue)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Api));
            }

            if (Endpoint.IsEmpty())
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Endpoint));
            }

            if (ApiKey.IsEmpty())
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(ApiKey));
            }
        }
    }
}