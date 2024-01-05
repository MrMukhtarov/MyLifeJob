using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Business.Exceptions.User;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Services.Implements;

public class AppUserService : IAppUserService
{
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;

    public AppUserService(UserManager<AppUser> user, IMapper mapper)
    {
        _user = user;
        _mapper = mapper;
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
