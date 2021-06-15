using System.Collections.Generic;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.AuthorizationKey
{
    public class UpdateAuthorizationKeyStatusRequest : IRequest
    {
        public int Id { get; set; }

        public IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Id));
            }
        }
    }
}