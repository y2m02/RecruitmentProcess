using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface ICandidateRepository :
        ICanUpdateRepository<Candidate>,
        ICanDeleteRepository { }

    public class CandidateRepository :
        BaseRepository<Candidate>,
        ICandidateRepository
    {
        public CandidateRepository(RecruitmentManagementContext context) : base(context) { }

        public Task<List<Candidate>> GetAll()
        {
            return Context.Candidates
                .Include(x => x.Recruitment)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Candidate> Update(Candidate entity)
        {
            return Modify(
                entity.CandidateId,
                entity,
                new()
                {
                    nameof(entity.Name),
                    nameof(entity.PhoneNumber),
                    nameof(entity.Email),
                    nameof(entity.Curriculum),
                    nameof(entity.GitHub),
                }
            );
        }

        public Task Delete(int id) => Remove(new Candidate { CandidateId = id });

        protected override Task<Candidate> GetById(int id)
        {
            return Context.Candidates
                .Include(x => x.Recruitment)
                .AsNoTracking()
                .SingleAsync(x => x.CandidateId == id);
        }
    }
}
