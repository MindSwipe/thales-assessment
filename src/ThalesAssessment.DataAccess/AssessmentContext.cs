using Microsoft.EntityFrameworkCore;
using ThalesAssessment.DataAccess.Configurations;
using ThalesAssessment.Entities;

namespace ThalesAssessment.DataAccess;

public class AssessmentContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    public DbSet<Role> Roles { get; set; }

    public AssessmentContext(DbContextOptions<AssessmentContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}
