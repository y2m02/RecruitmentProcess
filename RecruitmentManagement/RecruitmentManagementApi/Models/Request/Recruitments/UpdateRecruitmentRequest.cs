using System;
using System.Collections.Generic;
using System.Linq;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Recruitments
{
    public class UpdateRecruitmentRequest : IRequest
    {
        public int Id { get; set; }

        public RecruitmentStatus ? Status { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Id));
            }

            if (!Status.HasValue)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Status));
            }

            var allowedValues = Enum
                .GetValues(typeof(RecruitmentStatus))
                .Cast<RecruitmentStatus>()
                .EagerSelect(status => (int)status);

            if (Status.HasValue && !allowedValues.Contains((int)Status))
            {
                yield return ConsumerMessages
                    .AllowedValues
                    .Format(
                        nameof(Status),
                        allowedValues.Join(",")
                    );
            }
        }
    }
}