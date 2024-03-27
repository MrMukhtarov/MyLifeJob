using MyLifeJob.Business.Dtos.RequirementDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IRequirementService
{
    Task CreateAsync(RequirementCreateDto dto, int id);
    Task UpdateAsync(RequirementUpdateItemDto dto, int id);
    Task DeleteAsync(int id);
    Task<ICollection<RequirementListItemDto>> GetAllAsync();
    Task<RequirementDetailItemDto> GetByIdAsync(int id);
}
