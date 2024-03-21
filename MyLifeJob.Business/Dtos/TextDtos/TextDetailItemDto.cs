namespace MyLifeJob.Business.Dtos.TextDtos;

public record TextDetailItemDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int AdvertismentId { get; set; }
}
