using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    TokenResponseDto CreateUserToken(AppUser user, int expires = 60);
    string CreateRefreshToken();
}
