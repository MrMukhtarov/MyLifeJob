
using Microsoft.AspNetCore.Identity;

namespace MyLifeJob.Core.Entity;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresDate { get; set; }
    public EmailToken? EmailToken { get; set; }
    public Company? Company { get; set; }
}

