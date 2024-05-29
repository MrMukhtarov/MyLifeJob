using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record FilteredAdvertismentDto
{
    public FilterDate? Date { get; set; }
}
