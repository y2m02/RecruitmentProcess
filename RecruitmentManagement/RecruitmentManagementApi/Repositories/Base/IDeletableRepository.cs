using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface IDeletableRepository<in TModel>
    {
        Task Delete(TModel entity);
    }
}
