using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Request.Base
{
    public interface IRequest
    {
        public IEnumerable<string> Validate();
    }

    public interface IUpdateableRequest : IRequest
    {
        public int Id { get; set; }
    }
}