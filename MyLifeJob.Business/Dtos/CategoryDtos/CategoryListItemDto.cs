using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Dtos.AdvertismentDtos;

namespace MyLifeJob.Business.Dtos.CategoryDtos;

public record CategoryListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<AdvertismentForCategoryDto> Advertisments { get; set; }
}
