using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface ICanUpdateService
    {
        Task<Result> Update(IUpdateableRequest entity);
    }
}