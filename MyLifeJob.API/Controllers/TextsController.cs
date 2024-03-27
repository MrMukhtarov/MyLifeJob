using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TextsController : ControllerBase
{
    readonly ITextService _textService;

    public TextsController(ITextService textService)
    {
        _textService = textService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _textService.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _textService.GetByIdAsync(id));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] TextCreateDto dto, int adverId)
    {
        await _textService.CreateAsync(dto, adverId);
        return Ok();
    }

    //[HttpPut("[action]")]
    //public async Task<IActionResult> Update([FromForm] TextUpdateItemDto dto)
    //{
    //    await _textService.UpdateAsync(dto);
    //    return Ok();
    //}

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _textService.DeleteAsync(id);
        return Ok();
    }
}
