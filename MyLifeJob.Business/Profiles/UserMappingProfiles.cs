﻿using AutoMapper;
using MyLifeJob.Business.Dtos.UserDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}
