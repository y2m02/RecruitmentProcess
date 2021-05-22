using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        BaseResponse GetAll();
        BaseResponse Upsert(BaseRequest entity);
        BaseResponse Delete(BaseRequest entity);
    }
}
