using Intaker.TMS.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intaker.TMS.Dal.Configuration;

public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder
            .Property(x => x.Name).HasMaxLength(100);
        builder
            .Property(x => x.Description).HasMaxLength(255);
        builder
            .Property(x => x.AssignedTo).IsRequired(false).HasMaxLength(100);
        builder
            .Property(x => x.Status).IsRequired();
    }
}
