using System.ComponentModel;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.Requests
{
    public class UpdateRecruitmentRequest : BaseRequest
    {
        [DisplayName("Estado")]
        public RecruitmentStatus? Status { get; set; }

        [DisplayName("Comentario")]
        public string Note { get; set; }
    }
}