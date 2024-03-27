using MyLifeJob.Business.Dtos.AbilityDtos;
using MyLifeJob.Business.Dtos.RequirementDtos;
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record AdvertismentListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string City { get; set; }
    public decimal? Salary { get; set; }
    public string WorkGraphic { get; set; }
    public DateTime EndTime { get; set; }
    public IEnumerable<AdvertismentAbilityDto>? AdvertismentAbilities { get; set; }
    public ICollection<TextListItemDtos> Texts { get; set; }
    public ICollection<RequirementListItemDto> Requirements { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
    public int ViewCount { get; set; }
    public string Status { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryId { get; set; }
    public string State { get; set; }
}
