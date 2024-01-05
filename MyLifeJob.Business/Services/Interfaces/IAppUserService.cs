using MyLifeJob.Business.Dtos.UserDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAppUserService
{
    Task Register(RegisterDto dto);
}
