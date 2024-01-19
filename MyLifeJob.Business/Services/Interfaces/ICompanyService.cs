using MyLifeJob.Business.Dtos.CompanyDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface ICompanyService
{
    Task CreateAsync(CompanyCreateDto dto);
    Task<ICollection<CompanyListItemDto>> GetAllAsync(bool takeAll);
    Task<CompanyDetailItemDto> GetByIdAsync(int id, bool takeAll);
    Task UpdateAsync(int id, CompanyUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
