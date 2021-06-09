using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentManagementApi.Repositories.Base
{
    public interface IBaseRepository<TModel>
    {
        Task<List<TModel>> GetAll();
        Task Create(TModel entity);
    }
}
