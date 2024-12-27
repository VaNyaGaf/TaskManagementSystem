using Intaker.TMS.Dal.Configuration;
using Intaker.TMS.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Intaker.TMS.Dal;

public class WorkTaskContext : DbContext
{
    public WorkTaskContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<WorkTask> WorkTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkTask>().ToTable("WorkTasks");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkTaskConfiguration).Assembly);
    }
}
