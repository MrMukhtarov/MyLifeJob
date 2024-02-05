using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(a => a.Name)
            .IsRequired();
        builder.Property(a => a.Surname)
            .IsRequired();
        builder.HasOne(a => a.Company)
            .WithOne(a => a.AppUser)
            .HasForeignKey<Company>(a => a.AppUserId)
            .IsRequired();
    }
}
