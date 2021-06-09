using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface ICanUpdateRepository<TModel> : IBaseRepository<TModel>
    {
        Task Update(TModel entity);
    }
}
