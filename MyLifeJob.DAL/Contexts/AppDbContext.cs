using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Configurations;

namespace MyLifeJob.DAL.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<EmailToken> EmailTokens { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyIndustry> CompanyIndustries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Advertisment> Advertisments { get; set; }
    public DbSet<AdvertismentAbility> AdvertismentAbilities { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Requirement> Requirements { get; set; }
}
