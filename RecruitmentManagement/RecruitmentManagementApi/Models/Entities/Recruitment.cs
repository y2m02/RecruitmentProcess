using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecruitmentManagementApi.Models.Enums;

namespace RecruitmentManagementApi.Models.Entities
{
    public class Recruitment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecruitmentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public int CandidateId { get; set; }

        [Required]
        public RecruitmentStatus Status { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }

        public ICollection<RecruitmentUpdateHistory> RecruitmentUpdateHistories { get; set; }
    }
}
