using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        Task<Result> GetAll();
        Task<Result> Create(IRequest entity);
    }
}