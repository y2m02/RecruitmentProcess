using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagementApi.Models.Entities
{
    public class RecruitmentManagementContext : DbContext
    {
        public RecruitmentManagementContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<RecruitmentUpdateHistory> RecruitmentUpdateHistories { get; set; }
        public DbSet<AuthorizationKey> AuthorizationKeys { get; set; }
    }
}
