using System.ComponentModel;

namespace RecruitmentManagementApp.Models.Requests
{
    public class CandidateRequest : BaseRequest
    {
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Currículo")]
        public string Curriculum { get; set; }

        [DisplayName("GitHub")]
        public string GitHub { get; set; }
    }
}