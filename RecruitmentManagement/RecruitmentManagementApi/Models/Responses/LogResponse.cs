using System;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class LogResponse : BaseResponse
    {
        public DateTime RunAt { get; set; }
        public string Api { get; set; }
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
        public int ? AffectedEntity { get; set; }
    }
}