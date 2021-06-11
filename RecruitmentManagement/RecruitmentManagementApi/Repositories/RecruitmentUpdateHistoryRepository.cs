using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IRecruitmentUpdateHistoryRepository :
        IBaseRepository<RecruitmentUpdateHistory>
    {
        Task<List<RecruitmentUpdateHistory>> GetByRecruitmentId(int recruitmentId);
        Task<List<RecruitmentUpdateHistory>> GetAllByRecruitmentId(int recruitmentId);
        Task UpdateLastHistoryNote(int recruitmentId, string note);
        Task BatchDelete(IEnumerable<RecruitmentUpdateHistory> entities);
    }

    public class RecruitmentUpdateHistoryRepository :
        BaseRepository<RecruitmentUpdateHistory>,
        IRecruitmentUpdateHistoryRepository
    {
        public RecruitmentUpdateHistoryRepository(
            RecruitmentManagementContext context
        ) : base(context) { }

        public Task<List<RecruitmentUpdateHistory>> GetAll()
        {
            return Context.RecruitmentUpdateHistories
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<RecruitmentUpdateHistory>> GetByRecruitmentId(int recruitmentId)
        {
            return Context.RecruitmentUpdateHistories
                .AsNoTracking()
                .Where(x => x.RecruitmentId == recruitmentId)
                .OrderByDescending(r => r.Date)
                .Skip(1)
                .ToListAsync();
        }

        public Task<List<RecruitmentUpdateHistory>> GetAllByRecruitmentId(int recruitmentId)
        {
            return Context.RecruitmentUpdateHistories
                .AsNoTracking()
                .Where(x => x.RecruitmentId == recruitmentId)
                .ToListAsync();
        }

        public async Task UpdateLastHistoryNote(int recruitmentId, string note)
        {
            var history = await Context
                .RecruitmentUpdateHistories
                .OrderByDescending(x => x.Date)
                .FirstAsync(x => x.RecruitmentId == recruitmentId)
                .ConfigureAwait(false);

            history.Note = note;

            await Save().ConfigureAwait(false);
        }

        public Task BatchDelete(IEnumerable<RecruitmentUpdateHistory> entities)
        {
            return Remove(entities);
        }
    }
}
