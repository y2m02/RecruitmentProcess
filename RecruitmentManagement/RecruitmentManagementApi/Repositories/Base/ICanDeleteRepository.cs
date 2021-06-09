using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface ICanDeleteRepository<in TModel>
    {
        Task Delete(TModel entity);
    }
}
