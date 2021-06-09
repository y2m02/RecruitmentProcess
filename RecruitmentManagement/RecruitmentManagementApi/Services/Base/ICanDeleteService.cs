using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface ICanDeleteService
    {
        Task<Result> Delete(IRequest entity);
    }
}
