using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.DAL.Configurations;

public class TextConfigyration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder.Property(t => t.Content).IsRequired();
        builder.HasOne(a => a.Advertisment).WithMany(a => a.Texts).HasForeignKey(a => a.AdvertismentId);
    }
}
