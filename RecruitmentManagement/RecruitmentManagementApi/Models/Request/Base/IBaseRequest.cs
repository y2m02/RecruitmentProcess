using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Request.Base
{
    public interface IBaseRequest
    {
        public IEnumerable<string> Validate();
    }
}