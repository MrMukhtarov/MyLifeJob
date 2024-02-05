using MyLifeJob.Business.Dtos.CompanyDtos;

namespace MyLifeJob.Business.Dtos.IndustiryDtos;

public record IndustryListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<IndustryCompanyDto> CompanyIndustries { get; set; }
}
