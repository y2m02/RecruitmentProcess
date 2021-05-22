using System.Collections.Generic;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request
{
    public class StatusRequest : BaseRequest
    {
        public string Description { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Description.IsEmpty())
            {
                yield return UserMessages.FieldRequired.Format("Descripción");
            }
        }
    }
}