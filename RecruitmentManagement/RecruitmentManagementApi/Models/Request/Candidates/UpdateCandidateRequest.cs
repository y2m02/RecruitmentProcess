using HelpersLibrary.Extensions;
using System.Collections.Generic;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Candidates
{
    public class UpdateCandidateRequest : 
        CandidateRequest, 
        IUpdateableRequest
    {
        public int Id { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Id <= 0)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Id));
            }

            foreach (var validationError in base.Validate())
            {
                yield return validationError;
            }
        }
    }
}
