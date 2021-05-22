using RecruitmentManagementApi.Models.ViewModels.Base;

namespace RecruitmentManagementApi.Services.Base
{
    public interface IBaseService
    {
        BaseReturnViewModel GetAll();
        BaseReturnViewModel Upsert(BaseViewModel entity);
        BaseReturnViewModel Delete(BaseViewModel entity);
    }
}
