using System;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class CandidateResponse : BaseResponse
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Curriculum { get; set; }

        public string GitHub { get; set; }

        public int RecruitmentId { get; set; }

        public RecruitmentStatus RecruitmentStatus { get; set; }
    }
}