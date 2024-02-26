using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record AdvertismentForCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string City { get; set; }
    public decimal? Salary { get; set; }
    public string WorkGraphic { get; set; }
    public DateTime EndTime { get; set; }
    public string? Ability { get; set; }
    public string Text { get; set; }
    public string Requirement { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
    public int ViewCount { get; set; }
    public string Status { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; }
    public string State { get; set; }
}
