using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Business.Dtos.UserDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAppUserService
{
    Task Register(RegisterDto dto);
    Task<TokenResponseDto> Login(LoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshToken(string refreshToken);
    Task UpdateAsync(UpdateUserDto dto);
    Task ChangePassword(UpdateUserPasswordDto dto);
    Task<ICollection<UserWithRolesDto>> GetAllAsync(bool takeAll);
    Task<UserWithRole> GetByIdAsync(string id, bool takeAll);
    Task SoftDeleteAsync(string id);
    Task RevertSoftDelete(string id);
    Task DeleteAsync(string id);
    Task LogOut();
    Task AddRoleAsync(AddRoleDto dto);
}
