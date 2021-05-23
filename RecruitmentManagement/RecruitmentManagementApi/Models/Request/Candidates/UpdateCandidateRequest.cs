using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Extensions;

namespace RecruitmentManagementApi.Models.Request.Candidates
{
    public class UpdateCandidateRequest : CandidateRequest
    {
        public int Id { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format("Id");
            }

            foreach (var validationError in base.Validate())
            {
                yield return validationError;
            }
        }
    }
}
