
using Entites.Configuration;
using Microsoft.EntityFrameworkCore;

    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee> Companies { get; set; }

    }
