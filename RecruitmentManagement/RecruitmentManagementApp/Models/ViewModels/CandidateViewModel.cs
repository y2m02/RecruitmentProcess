using System;
using System.ComponentModel;
using HelpersLibrary.Extensions;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.ViewModels
{
    public class CandidateViewModel : BaseViewModel
    {
        [DisplayName("Nombre")]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Fecha")]
        public DateTime CreatedDate => Date.Date;

        [DisplayName("Currículo")]
        public string Curriculum { get; set; }

        [DisplayName("GitHub")]
        public string GitHub { get; set; }

        public int RecruitmentId { get; set; }

        public RecruitmentStatus RecruitmentStatus { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription => RecruitmentStatus.GetDescription();
    }
}
