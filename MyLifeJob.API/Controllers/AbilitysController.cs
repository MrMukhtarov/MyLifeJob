using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AbilitysController : ControllerBase
{
    readonly IAbilityService _service;

    public AbilitysController(IAbilityService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(string name)
    {
        await _service.CreateAsync(name);
        return Ok();
    }
    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
