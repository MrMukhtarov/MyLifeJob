using AutoMapper;
using MyLifeJob.Business.Dtos.CompanyDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<CompanyCreateDto, Company>().ReverseMap();
        CreateMap<CompanyUpdateDto, Company>().ReverseMap();
        CreateMap<CompanyIndustryDto, CompanyIndustry>().ReverseMap();
        CreateMap<Company, CompanyListItemDto>().ReverseMap();
        CreateMap<Company, CompanyDetailItemDto>().ReverseMap();
    }
}
