using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.RecruitmentUpdateHistories
{
    public class RecruitmentUpdateHistoryRequest : IRequest
    {
        public RecruitmentStatus Status { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Validate()
        {
            return new List<string>();
        }
    }
}