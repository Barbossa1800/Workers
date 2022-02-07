using Microsoft.EntityFrameworkCore;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Infrastructure.Context
{
    public class WorkerDbContext : DbContext
    {
        public WorkerDbContext(DbContextOptions<WorkerDbContext> options) : base(options)
        {
#if DEBUG
            Database.EnsureCreated();
#endif
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<StatusProject> StatusProjects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public DbSet<Status> Statuses { get; set; }

        /*new DbSet (authorization)*/
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public DbSet<Role> Roles{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Position>().Property(x => x.Id).HasColumnName("PositionId");
            //modelBuilder.Entity<>
        }
    }
}
