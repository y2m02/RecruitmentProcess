using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.Requests
{
    public class UpdateRecruitmentRequest : BaseRequest
    {
        public RecruitmentStatus? Status { get; set; }

        public string Note { get; set; }
    }
}