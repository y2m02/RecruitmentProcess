using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        Task<BaseResult> GetAll<TResponse>();
        Task<BaseResult> Create(IBaseRequest entity);
        Task<BaseResult> Update(IBaseRequest entity);
        Task<BaseResult> Delete(IBaseRequest entity);
    }
}