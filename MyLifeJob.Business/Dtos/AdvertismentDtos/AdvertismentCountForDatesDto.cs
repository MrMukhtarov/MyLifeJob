namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record AdvertismentCountForDatesDto
{
    public int Day { get; set; }
    public int Week { get; set; }
    public int Month { get; set; }
}
