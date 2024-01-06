using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.TokenDtos;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Exceptions.User;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Services.Implements;

public class AppUserService : IAppUserService
{
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;
    readonly ITokenService _token;

    public AppUserService(UserManager<AppUser> user, IMapper mapper, ITokenService token)
    {
        _user = user;
        _mapper = mapper;
        _token = token;
    }

    public async Task<TokenResponseDto> Login(LoginDto dto)
    {
        var user = await _user.FindByNameAsync(dto.UserName);
        if (user == null) throw new UserNotFoundException();
        var result = await _user.CheckPasswordAsync(user, dto.Password);
        if (result == false) throw new UserNotFoundException();

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
}
