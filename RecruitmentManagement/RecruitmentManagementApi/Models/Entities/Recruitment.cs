using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? StatusId { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
    }
}
