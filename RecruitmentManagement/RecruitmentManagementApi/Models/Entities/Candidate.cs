using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementApi.Models.Entities
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Curriculum { get; set; }

        public string GitHub { get; set; }

        public Recruitment Recruitment { get; set; }
    }
}
