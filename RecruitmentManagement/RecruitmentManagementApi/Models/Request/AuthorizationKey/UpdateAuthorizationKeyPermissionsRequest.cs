using System.Collections.Generic;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.AuthorizationKey
{
    public class UpdateAuthorizationKeyPermissionsRequest : IRequest
    {
        public int Id { get; set; }

        public List<Permission> Permissions { get; set; } = new();

        public IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Id));
            }
        }
    }
}