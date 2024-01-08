using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Exceptions.User;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    readonly IAppUserService _service;
    readonly UserManager<AppUser> _userManager;
    readonly IEmailService _email;
    readonly AppDbContext _context;


    public UsersController(IAppUserService service, UserManager<AppUser> userManager, IEmailService email, AppDbContext context)
    {
        _service = service;
        _userManager = userManager;
        _email = email;
        _context = context;
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

        EmailToken e = new EmailToken();
        e.UserId = user.Id;
        e.Date = DateTime.Now;
        e.Token = token;
        await _context.EmailTokens.AddAsync(e);
        await _context.SaveChangesAsync();

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
    public async Task<IActionResult> Login([FromForm] LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) throw new UserNotFoundException();
        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (result == false) throw new UserNotFoundException();
        var emailToken = await _context.EmailTokens.SingleOrDefaultAsync(u => u.UserId == user.Id);
        if (user.EmailConfirmed == false)
        {
            if (user.Id == emailToken.UserId && emailToken.Date.AddHours(24) > DateTime.Now) throw new ItHasntBeen24HoursException();
            else
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Users", new { token, email = user.Email }, Request.Scheme);

                string emailContentTemplate = _email.GetEmailConfirmationTemplate("EmailConfirm.html");

                string emailContent = emailContentTemplate.Replace("{USERNAME}", user.UserName).Replace("{CONFIRMATION_LINK}", confirmationLink);

                var message = new Message(new string[] { user.Email! }, "Confirmation email link", emailContent);
                _email.SendEmail(message);
                emailToken.Token = token;
                emailToken.Date = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        return Ok(await _service.Login(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken([FromForm]string token)
    {
        return Ok(await _service.LoginWithRefreshToken(token));
    }

}
