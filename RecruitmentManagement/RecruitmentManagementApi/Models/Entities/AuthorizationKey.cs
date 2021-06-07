using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementApi.Models.Entities
{
    public class AuthorizationKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorizationKeyId { get; set; }

        [Required]
        [StringLength(40)]
        public string Key { get; set; }

        public string Permissions { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
