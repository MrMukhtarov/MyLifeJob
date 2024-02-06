using AutoMapper;
using MyLifeJob.Business.Dtos.CategoryDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        CreateMap<Category, CategoryListItemDto>().ReverseMap();
        CreateMap<Category, CategoryDetailItemDto>().ReverseMap();
    }
}
