using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementApi.Models.Entities
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public ICollection<Recruitment> Recruitments { get; set; }

        public ICollection<RecruitmentUpdateHistory> RecruitmentUpdateHistories { get; set; }
    }
}
