using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class ValidationResult : BaseResult
    {
        public ValidationResult(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}
