using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Constants;
using MyLifeJob.Business.Dtos.CategoryDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Exceptions.FileService;
using MyLifeJob.Business.Extensions;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.Business.Services.Implements;

public class CategoryService : ICategoryService
{
    readonly ICategoryRepository _repo;
    readonly IFileService _file;
    readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IFileService file, IMapper mapper)
    {
        _repo = repo;
        _file = file;
        _mapper = mapper;
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        if (await _repo.IsExistAsync(c => c.Name == dto.Name)) throw new IsExistException<Category>();

        if (!dto.LogoFile.IsTypeValid("image")) throw new TypeNotValidException();
        if (!dto.LogoFile.IsSizeValid(2)) throw new SizeNotValidException();

        var map = _mapper.Map<Category>(dto);
        map.LogoUrl = await _file.UploadAsync(dto.LogoFile, RootContsant.CategoryLogoRoot);

        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Category>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Category>();

        _file.Delete(entity.LogoUrl);

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<CategoryListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll == true)
        {
            return _mapper.Map<ICollection<CategoryListItemDto>>(_repo.GetAllAsync());
        }
        else
        {
            return _mapper.Map<ICollection<CategoryListItemDto>>(_repo.FindAllAsync(c => c.IsDeleted == false));
        }
    }

    public async Task<CategoryDetailItemDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id < 0) throw new IdIsNegativeException<Category>();
        if (takeAll == true)
        {
            var data = await _repo.GetSingleAsync(c => c.Id == id);
            if (data == null) throw new NotFoundException<Category>();
            return _mapper.Map<CategoryDetailItemDto>(data);
        }
        else
        {
            var data = await _repo.GetSingleAsync(c => c.Id == id && c.IsDeleted == false);
            if (data == null) throw new NotFoundException<Category>();
            return _mapper.Map<CategoryDetailItemDto>(data);

        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Category>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Category>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Category>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Category>();

        _repo.SofDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto dto)
    {
        if (id < 0) throw new IdIsNegativeException<Category>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Category>();

        var map = _mapper.Map(dto, entity);

        if (dto.LogoFile != null)
        {
            _file.Delete(entity.LogoUrl);
            if (!dto.LogoFile.IsTypeValid("image")) throw new TypeNotValidException();
            if (!dto.LogoFile.IsSizeValid(2)) throw new SizeNotValidException();
            map.LogoUrl = await _file.UploadAsync(dto.LogoFile, RootContsant.CategoryLogoRoot);
        }

        if (await _repo.IsExistAsync(c => c.Name == dto.Name && c.Id != id)) throw new IsExistException<Category>();

        await _repo.SaveAsync();
    }
}
