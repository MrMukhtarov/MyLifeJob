using AutoMapper;
using MyLifeJob.Business.Dtos.IndustiryDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class IdustiryMappingProfile : Profile
{
    public IdustiryMappingProfile()
    {
        CreateMap<IndustryCreateDto, Industry>().ReverseMap();
        CreateMap<IndustryUpdateDto, Industry>().ReverseMap();
        CreateMap<Industry, IndustryListItemDto>().ReverseMap();
        CreateMap<Industry, IndustryDetailDto>().ReverseMap();
        CreateMap<Industry, IndustryCompanySingleItemDto>().ReverseMap();
        CreateMap<IndustryCompanyDto, CompanyIndustry>().ReverseMap();
    }
}
