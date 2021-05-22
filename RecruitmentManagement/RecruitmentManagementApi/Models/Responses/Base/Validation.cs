using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Validation : BaseResponse
    {
        public Validation(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}
