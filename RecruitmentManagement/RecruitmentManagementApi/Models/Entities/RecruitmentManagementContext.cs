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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Recruitment>()
                .HasMany(x => x.RecruitmentUpdateHistories)
                .WithOne(x => x.Recruitment)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Candidate>()
                .HasOne(x => x.Recruitment)
                .WithOne(x => x.Candidate)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
