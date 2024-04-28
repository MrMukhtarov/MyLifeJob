namespace MyLifeJob.Business.Dtos.CompanyDtos;

public record CompanySingleItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
}
