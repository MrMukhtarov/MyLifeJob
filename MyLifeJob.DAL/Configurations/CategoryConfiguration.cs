using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired();
        builder.Property(c => c.LogoUrl)
            .IsRequired();
    }
}
