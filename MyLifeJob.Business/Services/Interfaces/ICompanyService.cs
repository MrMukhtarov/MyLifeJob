using MyLifeJob.Business.Dtos.CompanyDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface ICompanyService
{
    Task CreateAsync(CompanyCreateDto dto);
    Task<ICollection<CompanyListItemDto>> GetAllAsync(bool takeAll);
}
