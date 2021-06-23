using System;
using System.Collections.Generic;
using System.ComponentModel;
using HelpersLibrary.Extensions;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.ViewModels
{
    public class RecruitmentViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }

        [DisplayName("Fecha")]
        public DateTime CreatedDate => Date.Date;

        [DisplayName("Nota")]
        public string Note { get; set; }

        public int CandidateId { get; set; }

        [DisplayName("Nombre")]
        public string CandidateName { get; set; }

        public RecruitmentStatus Status { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription { get; set; }

        public List<RecruitmentUpdateHistoryViewModel> RecruitmentUpdateHistories { get; set; }
    }
}