using MyLifeJob.Business.Dtos.IndustiryDtos;

namespace MyLifeJob.Business.Dtos.CompanyDtos;

public record CompanyIndustryDto
{
    public IndustryListItemDto Industry { get; set; }
}
