using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class CompanyIndustryConfiguration : IEntityTypeConfiguration<CompanyIndustry>
{
    public void Configure(EntityTypeBuilder<CompanyIndustry> builder)
    {
        builder.HasOne(c => c.Company)
            .WithMany(c => c.CompanyIndustries)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.Industry)
            .WithMany(c => c.CompanyIndustries)
            .HasForeignKey(c => c.IndustryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
