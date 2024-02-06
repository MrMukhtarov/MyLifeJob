using MyLifeJob.Business.Dtos.CategoryDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface ICategoryService
{
    Task<ICollection<CategoryListItemDto>> GetAllAsync(bool takeAll);
    Task<CategoryDetailItemDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(int id, CategoryUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
