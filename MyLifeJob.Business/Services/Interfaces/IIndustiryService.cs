using MyLifeJob.Business.Dtos.IndustiryDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IIndustiryService
{
    Task CreateAsync(IndustryCreateDto dto);
    Task<ICollection<IndustryListItemDto>> GetAllAsync(bool takeAll);
    Task<IndustryDetailDto> GetByIdAsync(int id, bool takeAll);
    Task UpdateAsync(int id, IndustryUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
