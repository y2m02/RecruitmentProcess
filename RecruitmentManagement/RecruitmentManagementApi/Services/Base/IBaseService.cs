using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        Task<Result> GetAll<TResponse>() where TResponse : BaseResponse;
        Task<Result> Create(IRequest entity);
        Task<Result> Update(IRequest entity);
        Task<Result> Delete(IRequest entity);
    }
}