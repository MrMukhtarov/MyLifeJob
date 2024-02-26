using MyLifeJob.Business.Dtos.AbilityDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAbilityService
{
    Task CreateAsync(string Name);
    Task DeleteAsync(int id);
    Task<ICollection<AbilityListItemDto>> GetAllAsync();
    Task<AbilityDetailDto> GetByIdAsync(int id);
}
