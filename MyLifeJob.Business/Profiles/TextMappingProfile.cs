using AutoMapper;
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Profiles;

public class TextMappingProfile : Profile
{
    public TextMappingProfile()
    {
        CreateMap<TextCreateDto, Text>().ReverseMap();
        CreateMap<TextUpdateItemDto, Text>().ReverseMap();
        CreateMap<Text, TextListItemDtos>().ReverseMap();
        CreateMap<Text, TextDetailItemDto>().ReverseMap();
    }
}
