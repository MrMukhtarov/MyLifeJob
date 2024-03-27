namespace MyLifeJob.Business.Dtos.RequirementDtos;

public record RequirementDetailItemDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int AdvertismentId { get; set; }
}
