using AutoMapper;
using MyLifeJob.Business.Dtos.AbilityDtos;
using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class AbilityMappingProfile : Profile
{
    public AbilityMappingProfile()
    {
        CreateMap<Ability, AbilityListItemDto>().ReverseMap();
        CreateMap<Ability, AbilityDetailDto>().ReverseMap();
        CreateMap<AdvertismentAbilityDto, AdvertismentAbility>().ReverseMap();
    }
}
