using Microsoft.AspNetCore.Identity;
using MyLifeJob.Business.Dtos.RoleDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IRoleService
{
    Task CreateAsync(string name);
    Task<ICollection<IdentityRole>> GetAllAsync();
    Task UpdateAsync(UpdateRoleDto dto);
    Task DeleteAsync(string name);
}
