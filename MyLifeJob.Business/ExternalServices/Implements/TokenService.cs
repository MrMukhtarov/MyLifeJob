using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Core.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyLifeJob.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    readonly IConfiguration _configuration;
    readonly UserManager<AppUser> _userManager;

    public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public TokenResponseDto CreateUserToken(AppUser user, int expires = 60)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var item in _userManager.GetRolesAsync(user).Result)
        {
            claims.Add(new Claim(ClaimTypes.Role, item));
        }

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigninKey"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtSecurity = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            DateTime.UtcNow.AddHours(4),
            DateTime.UtcNow.AddHours(4).AddMinutes(expires),
            credentials
            );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        string token = tokenHandler.WriteToken(jwtSecurity);
        string refreshToken = CreateRefreshToken();
        var refreshTokenExpires = jwtSecurity.ValidTo.AddMinutes(expires / 3);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresDate = refreshTokenExpires;
        _userManager.UpdateAsync(user).Wait();
        return new()
        {
            Token = token,
            RefreshToken = refreshToken,
            Expires = jwtSecurity.ValidTo,
            Username = user.UserName,
            RefreshTokenExpires = refreshTokenExpires,
            Roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }
}
