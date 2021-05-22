using System.Collections.Generic;
using System.ComponentModel;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services
{
    public class StatusResponse : BaseResponse
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
