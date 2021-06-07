using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface IUpdatableRepository<TModel> : IBaseRepository<TModel>
    {
        Task Update(TModel entity);
    }
}
