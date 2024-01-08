using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Exceptions.User;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using System.Security.Claims;

namespace MyLifeJob.Business.Services.Implements;

public class AppUserService : IAppUserService
{
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;
    readonly ITokenService _token;
    readonly IHttpContextAccessor _accessor;
    readonly string? _userId;

    public AppUserService(UserManager<AppUser> user, IMapper mapper, ITokenService token, IHttpContextAccessor accessor)
    {
        _user = user;
        _mapper = mapper;
        _token = token;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async Task<TokenResponseDto> Login(LoginDto dto)
    {
        var user = await _user.FindByNameAsync(dto.UserName);
        if (user == null) throw new UserNotFoundException();
        var result = await _user.CheckPasswordAsync(user, dto.Password);
        if (result == false) throw new UserNotFoundException();

        return _token.CreateUserToken(user);
    }

    public async Task<TokenResponseDto> LoginWithRefreshToken(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException();
        var user = await _user.Users.SingleOrDefaultAsync(s => s.RefreshToken ==  refreshToken);
        if(user == null) throw new UserNotFoundException();
        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new RefreshTokenExpiresIsOldException();
        return _token.CreateUserToken(user);
    }

    public async Task Register(RegisterDto dto)
    {
        if (await _user.Users.AnyAsync(u => u.Email == dto.Email)) throw new EmailAlreadyExistException();
        if (await _user.Users.AnyAsync(u => u.UserName == dto.Username)) throw new UserNameAlreadyExistExcepion();
        if (dto.Password != dto.ConfirmPassword) throw new PasswordDoesntMatchException();

        var user = _mapper.Map<AppUser>(dto);

        var res = await _user.CreateAsync(user, dto.Password);
        if (!res.Succeeded) throw new RegisterFailedException();
    }

    public async Task UpdateAsync(UpdateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(_userId)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(_userId);
        if(user == null) throw new UserNotFoundException();

        if (await _user.Users.AnyAsync(u => u.UserName == dto.UserName && u.Id != _userId || u.Email == dto.Email && u.Id != _userId))
            throw new UserIsExistException();

        var map = _mapper.Map(dto, user);
        var res = await _user.UpdateAsync(map);
        if (!res.Succeeded) throw new UserProfileUpdateInvalidException();
    }
}
