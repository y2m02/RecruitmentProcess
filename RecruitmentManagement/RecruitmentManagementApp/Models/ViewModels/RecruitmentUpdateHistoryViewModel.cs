using System;
using System.ComponentModel;
using HelpersLibrary.Extensions;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.ViewModels
{
    public class RecruitmentUpdateHistoryViewModel : BaseViewModel
    {
        public int RecruitmentId { get; set; }

        public int CandidateId { get; set; }

        [DisplayName("Nombre")]
        public string CandidateName { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Fecha")]
        public DateTime CreatedDate => Date.Date;

        [DisplayName("Nota")]
        public string Note { get; set; }

        public RecruitmentStatus Status { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription => Status.GetDescription();
    }
}