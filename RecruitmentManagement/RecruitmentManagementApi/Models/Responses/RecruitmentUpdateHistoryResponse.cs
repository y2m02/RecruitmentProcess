using System;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class RecruitmentUpdateHistoryResponse : BaseResponse
    {
        public int RecruitmentId { get; set; }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public RecruitmentStatus Status { get; set; }
    }
}