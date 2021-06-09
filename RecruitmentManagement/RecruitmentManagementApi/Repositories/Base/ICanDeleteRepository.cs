using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface ICanDeleteRepository
    {
        Task Delete(int id);
    }
}
