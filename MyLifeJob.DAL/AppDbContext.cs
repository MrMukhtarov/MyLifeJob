using Microsoft.EntityFrameworkCore;

namespace MyLifeJob.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
