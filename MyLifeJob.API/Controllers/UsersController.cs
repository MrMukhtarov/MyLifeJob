using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Exceptions.User;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using System.Security.Claims;

namespace MyLifeJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    readonly IAppUserService _service;
    readonly UserManager<AppUser> _userManager;
    readonly IEmailService _email;
    readonly AppDbContext _context;
    readonly IHttpContextAccessor _accessor;
    readonly string? _userId;


    public UsersController(IAppUserService service, UserManager<AppUser> userManager, IEmailService email, AppDbContext context,
        IHttpContextAccessor accessor)
    {
        _service = service;
        _userManager = userManager;
        _email = email;
        _context = context;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
    public async Task<IActionResult> LoginWithRefreshToken([FromForm] string token)
    {
        return Ok(await _service.LoginWithRefreshToken(token));
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserDto dto)
    {
        var email = await _userManager.Users.AnyAsync(u => u.Email == dto.Email);
        if (string.IsNullOrWhiteSpace(_userId)) throw new ArgumentNullException();
        var users = await _userManager.FindByIdAsync(_userId);
        if (users == null) throw new UserNotFoundException();
        if (!email)
        {
            users.EmailConfirmed = false;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(users);
            var confirmationLink = Url.Action("ConfirmEmail", "Users", new { token, email = dto.Email }, Request.Scheme);
            var emailToken = await _context.EmailTokens.SingleOrDefaultAsync(u => u.UserId == users.Id);

            emailToken.Token = token;
            emailToken.Date = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            string emailContentTemplate = _email.GetEmailConfirmationTemplate("EmailConfirm.html");

            string emailContent = emailContentTemplate.Replace("{USERNAME}", users.UserName).Replace("{CONFIRMATION_LINK}",
                confirmationLink);

            var message = new Message(new string[] { dto.Email! }, "Confirmation email link", emailContent);
            _email.SendEmail(message);
        }
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangePassword([FromForm] UpdateUserPasswordDto dto)
    {
        await _service.ChangePassword(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        await _service.SoftDeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> RevertSoftDelete(string id)
    {
        await _service.RevertSoftDelete(id);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LogOut()
    {
        await _service.LogOut();
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm]AddRoleDto dto)
    {
        await _service.AddRoleAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm]RemoveRoleDto dto)
    {
        await _service.RemoveRoleAsync(dto);
        return Ok();
    }
}

