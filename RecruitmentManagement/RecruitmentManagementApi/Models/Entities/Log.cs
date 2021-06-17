using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecruitmentManagementApi.Models.Enums;

namespace RecruitmentManagementApi.Models.Entities
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Required]
        public DateTime RunAt { get; set; }

        [Required]
        public Api Api { get; set; }

        [Required]
        [StringLength(50)]
        public string Endpoint { get; set; }

        [Required]
        [StringLength(50)]
        public string ApiKey { get; set; }

        public int ? AffectedEntity { get; set; }
    }
}