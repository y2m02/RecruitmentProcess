using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.AuthorizationKey
{
    public class AuthorizationKeyRequest : IRequest
    {  
        public string Key { get; set; }

        public bool IsActive { get; set; }

        public List<Permission> Permissions { get; set; } = new();

        public IEnumerable<string> Validate()
        {
            if (Key.Length != 40)
            {
                yield return ConsumerMessages.InvalidKeyLength;
            }
        }
    }
}