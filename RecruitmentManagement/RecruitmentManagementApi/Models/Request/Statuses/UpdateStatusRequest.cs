using System.Collections.Generic;
using RecruitmentManagementApi.Models.Extensions;

namespace RecruitmentManagementApi.Models.Request.Statuses
{
    public class UpdateStatusRequest : StatusRequest
    {
        public int Id { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return UserMessages.FieldRequired.Format("Id");
            }

            foreach (var validationError in base.Validate())
            {
                yield return validationError;
            }
        }
    }
}