using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Recruitments
{
    public class RecruitmentRequest : IRequest
    {
        public RecruitmentStatus Status { get; set; }

        public string Note { get; set; }

        public virtual IEnumerable<string> Validate()
        {
            return new List<string>();
        }
    }
}