using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class AdvertismentAbilityConfiguration : IEntityTypeConfiguration<AdvertismentAbility>
{
    public void Configure(EntityTypeBuilder<AdvertismentAbility> builder)
    {
        builder.HasOne(a => a.Ability)
            .WithMany(a => a.AdvertismentAbilities)
            .HasForeignKey(a => a.AbilityId);
        builder.HasOne(a => a.Advertisment)
            .WithMany(a => a.AdvertismentAbilities)
            .HasForeignKey(a => a.AdvertismentId);
    }
}
