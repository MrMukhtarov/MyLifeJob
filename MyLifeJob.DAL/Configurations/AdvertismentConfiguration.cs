using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class AdvertismentConfiguration : IEntityTypeConfiguration<Advertisment>
{
    public void Configure(EntityTypeBuilder<Advertisment> builder)
    {
        builder.Property(a => a.Title).IsRequired();
        builder.Property(a => a.City).IsRequired();
        builder.Property(a => a.WorkGraphic).IsRequired();
        builder.Property(a => a.EndTime).IsRequired();
        builder.Property(a => a.Text).IsRequired();
        builder.Property(a => a.CreateDate).HasDefaultValueSql("DATEADD(hour,4,GETUTCDATE())");
        builder.Property(a => a.Requirement).IsRequired();
        builder.HasOne(a => a.Category).WithMany(a => a.Advertisments).HasForeignKey(a => a.CategoryId);
        builder.HasOne(a => a.Company).WithMany(a => a.Advertisments).HasForeignKey(a => a.CompanyId);
    }
}
