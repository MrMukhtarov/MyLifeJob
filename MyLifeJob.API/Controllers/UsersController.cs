using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Services.Interfaces;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    readonly IAppUserService _service;

    public UsersController(IAppUserService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromForm]RegisterDto dto)
    {
        await _service.Register(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
}
