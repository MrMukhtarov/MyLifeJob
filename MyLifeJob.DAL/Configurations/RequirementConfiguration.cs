using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class RequirementConfiguration : IEntityTypeConfiguration<Requirement>
{
    public void Configure(EntityTypeBuilder<Requirement> builder)
    {
        builder.Property(r => r.Content).IsRequired();
        builder.HasOne(r => r.Advertisment).WithMany(r => r.Requirements).HasForeignKey(r => r.AdvertismentId);
    }
}
