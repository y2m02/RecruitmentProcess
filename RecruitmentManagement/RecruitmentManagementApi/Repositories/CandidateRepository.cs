using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories.Base;

namespace RecruitmentManagementApi.Repositories
{
    public interface ICandidateRepository : IBaseRepository<Candidate> { }

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

        public async Task Create(Candidate entity)
        {
            await Context.Candidates.AddAsync(entity).ConfigureAwait(false);

            await Save().ConfigureAwait(false);
        }

        public async Task Update(Candidate entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(
                entity,
                new()
                {
                    nameof(entity.Name), 
                    nameof(entity.Curriculum), 
                    nameof(entity.GitHub),
                }
            );

            await SaveChangesAndDetach(entity).ConfigureAwait(false);
        }

        public Task Delete(Candidate entity)
        {
            return Remove(entity);
        }
    }
}