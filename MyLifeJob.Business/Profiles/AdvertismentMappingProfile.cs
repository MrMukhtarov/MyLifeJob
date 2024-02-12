using AutoMapper;
using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class AdvertismentMappingProfile : Profile
{
    public AdvertismentMappingProfile()
    {
        CreateMap<AdvertismentCreateDto, Advertisment>().ReverseMap();
        CreateMap<AdvertismentUpdateDto, Advertisment>().ReverseMap();
        CreateMap<Advertisment, AdvertismentListItemDto>().ReverseMap();
        CreateMap<Advertisment, AdvertismentDetailItemDto>().ReverseMap();
        CreateMap<Advertisment, AdvertismentForCategoryDto>().ReverseMap();
    }
}
