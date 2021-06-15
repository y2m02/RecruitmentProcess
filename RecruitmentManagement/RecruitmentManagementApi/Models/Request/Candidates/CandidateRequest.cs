using System.Collections.Generic;
using System.Linq;
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

            if (PhoneNumber.IsNotEmpty() && (PhoneNumber.Length != 10 || !PhoneNumber.All(char.IsDigit)))
            {
                yield return ConsumerMessages.InvalidField.Format(nameof(PhoneNumber));
            }

            if (
                 Email.IsNotEmpty() &&
                (Email.Count(x => x == '@') != 1 || Email.Split('@').Any(x => x.Length < 1))
            )
            {
                yield return ConsumerMessages.InvalidField.Format(nameof(Email));
            }
        }
    }
}