using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.RequirementDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequirementsController : ControllerBase
{
    readonly IRequirementService _service;

    public RequirementsController(IRequirementService service)
    {
        _service = service;
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Create([FromForm] RequirementCreateDto dto, int id)
    {
        await _service.CreateAsync(dto, id);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
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

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] RequirementUpdateItemDto dto, int id)
    {
        await _service.UpdateAsync(dto, id);
        return Ok();
    }
}
