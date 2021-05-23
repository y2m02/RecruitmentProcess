using System.Threading.Tasks;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IRecruitmentUpdateHistoryRepository
    {
        Task Create(RecruitmentUpdateHistory entity);
    }

    public class RecruitmentUpdateHistoryRepository :
        BaseRepository<RecruitmentUpdateHistory>,
        IRecruitmentUpdateHistoryRepository
    {
        public RecruitmentUpdateHistoryRepository(
            RecruitmentManagementContext context
        ) : base(context) { }

        public Task Create(RecruitmentUpdateHistory entity) => Add(entity);
    }
}