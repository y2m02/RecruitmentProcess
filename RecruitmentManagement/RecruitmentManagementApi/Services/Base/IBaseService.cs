using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        Task<BaseResponse> GetAll<TResponse>();
        Task<BaseResponse> Upsert(BaseRequest entity);
        Task<BaseResponse> Delete(BaseResponse entity);
    }
}