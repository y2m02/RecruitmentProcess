using System.Collections.Generic;

namespace RecruitmentManagementApp.Models.ViewModels.Base
{
    public class Validation : BaseReturnViewModel
    {
        public Validation(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}
