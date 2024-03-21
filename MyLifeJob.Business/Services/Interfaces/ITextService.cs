using MyLifeJob.Business.Dtos.TextDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface ITextService
{
    Task<ICollection<TextListItemDtos>> GetAllAsync();
    Task<TextDetailItemDto> GetByIdAsync(int id);
    Task CreateAsync(TextCreateDto dto, int AdverId);
    Task UpdateAsync(TextUpdateItemDto dto);
    Task DeleteAsync(int id);
}
