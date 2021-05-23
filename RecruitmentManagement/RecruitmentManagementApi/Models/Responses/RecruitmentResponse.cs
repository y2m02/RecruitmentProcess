using System;
using System.Collections.Generic;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class RecruitmentResponse : BaseResponse
    {
        public DateTime Date { get; set; }

        public string Note { get; set; }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public RecruitmentStatus Status { get; set; }

        public List<RecruitmentUpdateHistory> RecruitmentUpdateHistories { get; set; }
    }
}