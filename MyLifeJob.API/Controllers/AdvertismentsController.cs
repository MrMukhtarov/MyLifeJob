using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Enums;

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

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(true, id));
    }


    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetByClient(int id)
    {
        return Ok(await _service.GetByIdAsync(false, id));
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] AdvertismentUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
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

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> AcceptState(int id)
    {
        await _service.AcceptState(id);
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> RejectState(int id)
    {
        await _service.RejectState(id);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAccept()
    {
        return Ok(await _service.AcceptGetall());
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateState(int id, State state)
    {
        await _service.ChangeState(id, state);
        return Ok();
    }
}
