using AutoMapper;
using MyLifeJob.Business.Dtos.IndustiryDtos;
using MyLifeJob.Business.Exceptions.Industry;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.DAL.Repositories.Interfaces;
using MyLifeJob.Business.Extensions;
using MyLifeJob.Business.Exceptions.FileService;
using MyLifeJob.Core.Entity;
using MyLifeJob.Business.Constants;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Exceptions.Commons;

namespace MyLifeJob.Business.Services.Implements;

public class IndustryService : IIndustiryService
{
    readonly IIndustiryRepository _repo;
    readonly IMapper _mapper;
    readonly IFileService _file;

    public IndustryService(IIndustiryRepository repo, IMapper mapper, IFileService file)
    {
        _repo = repo;
        _mapper = mapper;
        _file = file;
    }

    public async Task CreateAsync(IndustryCreateDto dto)
    {
        if (await _repo.IsExistAsync(i => i.Name == dto.Name)) throw new IndustryIsExistException();

        if (!dto.LogoFile.IsTypeValid("image")) throw new TypeNotValidException();
        if (!dto.LogoFile.IsSizeValid(3)) throw new SizeNotValidException();

        var map = _mapper.Map<Industry>(dto);
        map.Logo = await _file.UploadAsync(dto.LogoFile, RootContsant.IndustryLogoRoot);

        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<IndustryListItemDto>> GetAllAsync(bool takeAll)
    {
        var data = await _repo.GetAllAsync().ToListAsync();
        if (takeAll == true)
        {
            return _mapper.Map<ICollection<IndustryListItemDto>>(data);
        }
        else
        {
            var filteredData = data.Where(i => i.IsDeleted == false).ToList();
            return _mapper.Map<ICollection<IndustryListItemDto>>(filteredData);
        }
    }

    public async Task<IndustryDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Industry>();
        var ind = await _repo.FindByIdAsync(id);
        if (ind == null) throw new NotFoundException<Industry>();

        if (takeAll == true)
        {
            return _mapper.Map<IndustryDetailDto>(ind);
        }
        else
        {
            if (ind.IsDeleted == true) throw new NotFoundException<Industry>();
            return _mapper.Map<IndustryDetailDto>(ind);
        }
    }

    public async Task UpdateAsync(int id, IndustryUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Industry>();
        var ind = await _repo.FindByIdAsync(id);
        if (ind == null) throw new NotFoundException<Industry>();

        if (await _repo.IsExistAsync(i => i.Name == dto.Name && i.Id != id)) throw new IndustryIsExistException();

        if (dto.LogoFile != null)
        {
            _file.Delete(ind.Logo);
            if (!dto.LogoFile.IsTypeValid("image")) throw new TypeNotValidException();
            if (!dto.LogoFile.IsSizeValid(3)) throw new SizeNotValidException();
            ind.Logo = await _file.UploadAsync(dto.LogoFile, RootContsant.IndustryLogoRoot);
        }
        var map = _mapper.Map(dto, ind);
        await _repo.SaveAsync();
    }
}
