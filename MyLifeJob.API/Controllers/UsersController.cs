using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    readonly IAppUserService _service;
    readonly UserManager<AppUser> _userManager;
    readonly IEmailService _email;


    public UsersController(IAppUserService service, UserManager<AppUser> userManager, IEmailService email)
    {
        _service = service;
        _userManager = userManager;
        _email = email;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        await _service.Register(dto);

        var user = await _userManager.FindByEmailAsync(dto.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Users", new { token, email = dto.Email }, Request.Scheme);

        string emailContentTemplate = _email.GetEmailConfirmationTemplate("EmailConfirm.html");

        string emailContent = emailContentTemplate.Replace("{USERNAME}", user.UserName).Replace("{CONFIRMATION_LINK}", confirmationLink);

        var message = new Message(new string[] { dto.Email! }, "Confirmation email link", emailContent);
        _email.SendEmail(message);

        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm]LoginDto dto)
    {
        return Ok(await _service.Login(dto));
    }

}
