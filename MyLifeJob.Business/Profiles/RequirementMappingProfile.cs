using AutoMapper;
using MyLifeJob.Business.Dtos.RequirementDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class RequirementMappingProfile : Profile
{
    public RequirementMappingProfile()
    {
        CreateMap<RequirementCreateDto, Requirement>().ReverseMap();
        CreateMap<RequirementUpdateItemDto, Requirement>().ReverseMap();
        CreateMap<Requirement, RequirementListItemDto>().ReverseMap();
        CreateMap<Requirement, RequirementDetailItemDto>().ReverseMap();
    }
}
