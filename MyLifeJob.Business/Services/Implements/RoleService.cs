using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.RoleDtos;
using MyLifeJob.Business.Exceptions.Role;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.Business.Services.Implements;

public class RoleService : IRoleService
{
    readonly RoleManager<IdentityRole> _role;

    public RoleService(RoleManager<IdentityRole> role)
    {
        _role = role;
    }

    public async Task CreateAsync(string name)
    {
        var roles = await _role.Roles.SingleOrDefaultAsync(r => r.Name == name);
        if (roles != null) throw new RoleIsExistException();

        IdentityRole role = new IdentityRole();
        role.Name = name;

        var res = await _role.CreateAsync(role);
        if (!res.Succeeded) throw new CreateRoleFailedException();
    }

    public async Task DeleteAsync(string name)
    {
        if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
        var exist = await _role.FindByNameAsync(name);
        if (exist == null) throw new RoleNotFoundException();
        var res = await _role.DeleteAsync(exist);
        if (!res.Succeeded) throw new RoleDeleteFailedException();
    }

    public async Task<ICollection<IdentityRole>> GetAllAsync()
    {
        ICollection<IdentityRole> roles = await _role.Roles.ToListAsync();
        return roles;
    }

    public async Task UpdateAsync(UpdateRoleDto dto)
    {
        var exist = await _role.Roles.SingleOrDefaultAsync(r => r.Name == dto.Name);
        if (exist == null) throw new RoleNotFoundException();
        if (await _role.Roles.SingleOrDefaultAsync(r => r.Name == dto.NewName) != null) throw new RoleIsExistException();
        exist.Name = dto.NewName;
        var res = await _role.UpdateAsync(exist);
        if (!res.Succeeded) throw new UpdateRoleFailedException();
    }
}
