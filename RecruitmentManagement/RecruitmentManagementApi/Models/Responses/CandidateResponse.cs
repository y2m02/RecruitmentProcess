using System;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class CandidateResponse : BaseResponse
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Curriculum { get; set; }

        public string GitHub { get; set; }

        public int StatusId { get; set; }

        public int StatusDescription { get; set; }
    }
}