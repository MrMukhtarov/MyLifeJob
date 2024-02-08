using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertismentsController : ControllerBase
{
    readonly IAdvertismentService _service;

    public AdvertismentsController(IAdvertismentService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] AdvertismentCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll(true));
    }
}
