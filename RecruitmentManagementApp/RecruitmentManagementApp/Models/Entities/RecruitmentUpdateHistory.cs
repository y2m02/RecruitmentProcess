using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentManagementApp.Models.Entities
{
    public class RecruitmentUpdateHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecruitmentUpdateHistoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [Required]
        [StringLength(100)]
        public string ChangeReason { get; set; }

        public int RecruitmentId { get; set; }

        public int? CandidateId { get; set; }

        public int? StatusId { get; set; }

        [ForeignKey(nameof(RecruitmentId))]
        public Recruitment Recruitment { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
    }
}
