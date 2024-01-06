using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Business.Dtos.UserDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAppUserService
{
    Task Register(RegisterDto dto);
    Task<TokenResponseDto> Login(LoginDto dto);
}
