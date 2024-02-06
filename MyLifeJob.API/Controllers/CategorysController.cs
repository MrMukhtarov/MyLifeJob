using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.CategoryDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategorysController : ControllerBase
{
    readonly ICategoryService _service;

    public CategorysController(ICategoryService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] CategoryCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] CategoryUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> RevertSoftDelete(int id)
    {
        await _service.RevertSoftDeleteAsync(id);
        return Ok();
    }
}
