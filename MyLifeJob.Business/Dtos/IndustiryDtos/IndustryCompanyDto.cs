using MyLifeJob.Business.Dtos.CompanyDtos;

namespace MyLifeJob.Business.Dtos.IndustiryDtos;

public record IndustryCompanyDto
{
    public CompanyIndustrySingleItemDto Company { get; set; }
}
