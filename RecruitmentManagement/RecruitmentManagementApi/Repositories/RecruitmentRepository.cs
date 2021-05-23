using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IRecruitmentRepository
    {
        Task<List<Recruitment>> GetAll();
        Task Update(Recruitment entity);
    }

    public class RecruitmentRepository :
        BaseRepository<Recruitment>,
        IRecruitmentRepository

    {
        public RecruitmentRepository(RecruitmentManagementContext context) : base(context) { }

        public Task<List<Recruitment>> GetAll()
        {
            return Context.Recruitments
                .Include(x => x.Candidate)
                .Include(x => x.RecruitmentUpdateHistories.OrderByDescending(r => r.Date).Skip(1))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Update(Recruitment entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(
                entity,
                new()
                {
                    nameof(entity.Status),
                    nameof(entity.Note),
                }
            );

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }
    }
}