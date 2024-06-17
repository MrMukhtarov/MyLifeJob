using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record FilteredAdvertismentDto
{
    public FilterDate? Date { get; set; }
    public Sort? Sort { get; set; }
    public SortSalary? SortSalary { get; set; }
    public string? City { get; set; }

}
