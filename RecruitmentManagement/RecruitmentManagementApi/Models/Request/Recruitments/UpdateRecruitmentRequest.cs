﻿using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Extensions;
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
        }
    }
}