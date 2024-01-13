using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.RoleDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    readonly IRoleService _role;

    public RolesController(IRoleService role)
    {
        _role = role;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(string name)
    {
        await _role.CreateAsync(name);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _role.GetAllAsync());
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromForm]UpdateRoleDto dto)
    {
        await _role.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string name)
    {
        await _role.DeleteAsync(name);
        return Ok();
    }
}
