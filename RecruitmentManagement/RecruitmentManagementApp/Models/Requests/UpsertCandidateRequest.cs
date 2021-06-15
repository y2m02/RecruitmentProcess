using System.ComponentModel;

namespace RecruitmentManagementApp.Models.Requests
{
    public class UpsertCandidateRequest : BaseRequest
    {
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Teléfono")]
        public string PhoneNumber { get; set; }

        [DisplayName("Correo")]
        public string Email { get; set; }

        [DisplayName("Currículo")]
        public string Curriculum { get; set; }

        [DisplayName("GitHub")]
        public string GitHub { get; set; }
    }
}