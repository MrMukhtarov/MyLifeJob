namespace MyLifeJob.Business.Dtos.RequirementDtos;

public record RequirementListItemDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int AdvertismentId { get; set; }
}
