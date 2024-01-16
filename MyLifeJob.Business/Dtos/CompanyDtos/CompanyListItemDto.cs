using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.Dtos.CompanyDtos;

public record CompanyListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string? Website { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<CompanyIndustryDto> CompanyIndustries { get; set; }
}
