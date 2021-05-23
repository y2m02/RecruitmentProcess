using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Request.Base
{
    public interface IRequest
    {
        public IEnumerable<string> Validate();
    }
}