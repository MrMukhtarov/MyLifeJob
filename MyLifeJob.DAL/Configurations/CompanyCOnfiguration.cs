using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class CompanyCOnfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Phone).IsRequired();
        builder.Property(c => c.Location).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.Date).HasDefaultValueSql("DATEADD(hour,4,GETUTCDATE())");
    }
}
