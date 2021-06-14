using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.AuthorizationKey
{
    public class AuthorizationKeyRequest : IRequest
    {
        public bool IsActive { get; set; }

        public List<Permission> Permissions { get; set; } = new();

        public IEnumerable<string> Validate()
        {
            return new List<string>();
        }
    }
}