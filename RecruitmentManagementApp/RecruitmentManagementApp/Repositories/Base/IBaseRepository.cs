using System.Collections.Generic;

namespace RecruitmentManagementApp.Repositories.Base
{
    public interface IBaseRepository<TModel>
    {
        IEnumerable<TModel> GetAll();
        void Create(TModel entity);
        void Update(TModel entity);
        void Delete(TModel entity);
    }
}
