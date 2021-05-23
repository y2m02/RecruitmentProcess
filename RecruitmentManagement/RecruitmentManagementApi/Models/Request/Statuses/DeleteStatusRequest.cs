using System.Collections.Generic;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Statuses
{
    public class DeleteStatusRequest : IRequest
    {
        public int Id { get; set; }

        public IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format("Id");
            }
        }
    }
}