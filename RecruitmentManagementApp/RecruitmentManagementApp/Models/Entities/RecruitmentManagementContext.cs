using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagementApp.Models.Entities
{
    public class RecruitmentManagementContext : DbContext
    {
        public RecruitmentManagementContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<RecruitmentUpdateHistory> RecruitmentUpdateHistories { get; set; }
    }
}
