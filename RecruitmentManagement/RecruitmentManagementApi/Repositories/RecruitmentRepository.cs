using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface IRecruitmentRepository :
        ICanUpdateRepository<Recruitment>,
        ICanDeleteRepository
    {
        Task<RecruitmentStatus> GetStatus(int id);
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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RecruitmentStatus> GetStatus(int id)
        {
            return (await GetById(id).ConfigureAwait(false)).Status;
        }

        public Task<Recruitment> Update(Recruitment entity)
        {
            return Modify(
                entity.RecruitmentId,
                entity,
                new()
                {
                    nameof(entity.Status),
                    nameof(entity.Note),
                }
            );
        }

        public Task Delete(int id) => Remove(new Recruitment { RecruitmentId = id });

        protected override Task<Recruitment> GetById(int id)
        {
            return Context.Recruitments
                .Include(x => x.Candidate)
                .AsNoTracking()
                .SingleAsync(x => x.RecruitmentId == id);
        }
    }
}
