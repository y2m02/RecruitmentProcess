using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecruitmentManagementApi.Models.Enums;

namespace RecruitmentManagementApi.Models.Entities
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

        public int RecruitmentId { get; set; }
        
        [Required]
        public RecruitmentStatus Status { get; set; }

        [ForeignKey(nameof(RecruitmentId))]
        public Recruitment Recruitment { get; set; }
    }
}
