using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Validation : ResponseType
    {
        public Validation(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}
