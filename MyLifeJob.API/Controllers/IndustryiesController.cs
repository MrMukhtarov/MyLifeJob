using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.IndustiryDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndustryiesController : ControllerBase
{
    readonly IIndustiryService _service;

    public IndustryiesController(IIndustiryService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] IndustryCreateDto dto)
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
    public async Task<IActionResult> Update([FromForm] IndustryUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
