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
    readonly SignInManager<AppUser> _sign;

    public AppUserService(UserManager<AppUser> user, IMapper mapper, ITokenService token, IHttpContextAccessor accessor
, SignInManager<AppUser> sign)
    {
        _user = user;
        _mapper = mapper;
        _token = token;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _sign = sign;
    }

    public async Task ChangePassword(UpdateUserPasswordDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentException();
        var user = await _user.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException();

        var checkCurrent = await _user.CheckPasswordAsync(user, dto.CurrentPassword);
        if (checkCurrent == false) throw new CurrentPaswordIsInvalidException();

        var changePassword = await _user.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (!changePassword.Succeeded) throw new UpdatePasswordInvalidException();
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(id);
        if (user == null) throw new UserNotFoundException();
        var res = await _user.DeleteAsync(user);
        if (!res.Succeeded) throw new UserDeleteInvalidException();
    }

    public async Task<ICollection<ListItemUserDto>> GetAllAsync(bool takeAll)
    {
        var users = await _user.Users.ToListAsync();
        if (takeAll == true)
        {
            return _mapper.Map<ICollection<ListItemUserDto>>(users);
        }
        else
        {
            var filteredUsers = users.Select(u => u.IsDeleted == false);
            return _mapper.Map<ICollection<ListItemUserDto>>(filteredUsers);
        }
    }

    public async Task<SingleUserItemDto> GetByIdAsync(string id, bool takeAll)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentException();
        if (takeAll == true)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null) throw new UserNotFoundException();
            return _mapper.Map<SingleUserItemDto>(user);
        }
        else
        {
            var user = await _user.Users.SingleOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
            if (user == null) throw new UserNotFoundException();
            return _mapper.Map<SingleUserItemDto>(user);
        }
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
        var user = await _user.Users.SingleOrDefaultAsync(s => s.RefreshToken == refreshToken);
        if (user == null) throw new UserNotFoundException();
        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new RefreshTokenExpiresIsOldException();
        return _token.CreateUserToken(user);
    }

    public async Task LogOut()
    {
        await _sign.SignOutAsync();
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException();
        user.RefreshToken = null;
        user.RefreshTokenExpiresDate = null;
        await _user.UpdateAsync(user);
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

    public async Task RevertSoftDelete(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(id);
        if (user == null) throw new UserNotFoundException();
        user.IsDeleted = false;
        await _user.UpdateAsync(user);
    }

    public async Task SoftDeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(id);
        if (user == null) throw new UserNotFoundException();
        user.IsDeleted = true;
        await _user.UpdateAsync(user);
    }

    public async Task UpdateAsync(UpdateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(_userId)) throw new ArgumentNullException();
        var user = await _user.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException();

        if (await _user.Users.AnyAsync(u => u.UserName == dto.UserName && u.Id != _userId || u.Email == dto.Email && u.Id != _userId))
            throw new UserIsExistException();

        var map = _mapper.Map(dto, user);
        var res = await _user.UpdateAsync(map);
        if (!res.Succeeded) throw new UserProfileUpdateInvalidException();
    }
}
