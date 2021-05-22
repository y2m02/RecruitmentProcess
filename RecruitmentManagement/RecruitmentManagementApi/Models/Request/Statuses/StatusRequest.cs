using System.Collections.Generic;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Statuses
{
    public class StatusRequest : IBaseRequest
    {
        public string Description { get; set; }

        public virtual IEnumerable<string> Validate()
        {
            if (Description.IsEmpty())
            {
                yield return UserMessages.FieldRequired.Format("Descripción");
            }
        }
    }
}