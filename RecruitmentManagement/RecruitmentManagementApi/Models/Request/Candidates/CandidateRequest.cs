using System.Collections.Generic;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Request.Base;

namespace RecruitmentManagementApi.Models.Request.Candidates
{
    public class CandidateRequest : IRequest
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Curriculum { get; set; }

        public string GitHub { get; set; }

        public virtual IEnumerable<string> Validate()
        {
            if (Name.IsEmpty())
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Name));
            }
        }
    }
}