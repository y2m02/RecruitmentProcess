using System.Collections.Generic;
using System.ComponentModel;
using RecruitmentManagementApp.Models;
using RecruitmentManagementApp.Models.Extensions;
using RecruitmentManagementApp.Models.ViewModels.Base;

namespace RecruitmentManagementApp.Services
{
    public class StatusViewModel : BaseViewModel
    {
        /// <summary>
        ///     This property's going to be set automatically.
        ///     Please use Id instead.
        /// </summary>
        public int StatusId => Id;

        [DisplayName("Descripción")]
        public string Description { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Description.IsEmpty())
            {
                yield return UserMessages.FieldRequired.Format("Descripción");
            }
        }
    }
}
