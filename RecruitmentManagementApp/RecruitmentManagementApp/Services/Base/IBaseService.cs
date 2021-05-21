using RecruitmentManagementApp.Models.ViewModels.Base;

namespace RecruitmentManagementApp.Services.Base
{
    public interface IBaseService
    {
        BaseReturnViewModel GetAll();
        BaseReturnViewModel Upsert(BaseViewModel entity);
        BaseReturnViewModel Delete(BaseViewModel entity);
    }
}
