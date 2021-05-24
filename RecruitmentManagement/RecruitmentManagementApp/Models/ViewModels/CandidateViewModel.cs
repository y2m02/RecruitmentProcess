using System;
using HelpersLibrary.Extensions;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.ViewModels
{
    public class CandidateViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Curriculum { get; set; }

        public string GitHub { get; set; }

        public int RecruitmentId { get; set; }

        public RecruitmentStatus RecruitmentStatus { get; set; }

        public string StatusDescription => RecruitmentStatus.GetDescription();
    }
}
