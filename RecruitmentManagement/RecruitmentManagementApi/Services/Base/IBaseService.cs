using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        ResponseType GetAll();
        ResponseType Upsert(BaseResponse entity);
        ResponseType Delete(BaseResponse entity);
    }
}
