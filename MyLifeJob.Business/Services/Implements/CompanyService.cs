using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Constants;
using MyLifeJob.Business.Dtos.CompanyDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Exceptions.Company;
using MyLifeJob.Business.Exceptions.FileService;
using MyLifeJob.Business.Extensions;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;
using System.Security.Claims;

namespace MyLifeJob.Business.Services.Implements;

public class CompanyService : ICompanyService
{
    readonly ICompanyRepository _repo;
    readonly IMapper _map;
    readonly IFileService _file;
    readonly IIndustiryRepository _industry;
    private string? _userId;
    readonly IHttpContextAccessor _accessor;
    readonly UserManager<AppUser> _user;

    public CompanyService(ICompanyRepository repo, IMapper map, IFileService file, IIndustiryRepository industry,
        IHttpContextAccessor accessor, UserManager<AppUser> user)
    {
        _repo = repo;
        _map = map;
        _file = file;
        _industry = industry;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
    }

    public async Task CreateAsync(CompanyCreateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var user = await _user.Users.Include(u => u.Company).SingleOrDefaultAsync(u => u.Id == _userId);
        if (user == null) throw new NotFoundException<AppUser>();

        if (await _repo.IsExistAsync(c => c.Name == dto.Name)) throw new IsExistException<Company>();

        if (user.Company != null) throw new UserCompanyNotEmptyException();

        var map = _map.Map<Company>(dto);

        if (dto.LogoFIle != null)
        {
            if (!dto.LogoFIle.IsSizeValid(3)) throw new SizeNotValidException();
            if (!dto.LogoFIle.IsTypeValid("image")) throw new TypeNotValidException();
            map.Logo = await _file.UploadAsync(dto.LogoFIle, RootContsant.CompanyLogoRoot);
        }

        List<CompanyIndustry> ci = new List<CompanyIndustry>();
        foreach (var item in dto.IndustryIds)
        {
            if (await _industry.FindByIdAsync(item) == null) throw new NotFoundException<Industry>();
            var ind = await _industry.FindByIdAsync(item);
            if (ind == null) throw new NotFoundException<Industry>();
            ci.Add(new CompanyIndustry
            {
                Industry = ind,
            });
        }
        map.CompanyIndustries = ci;
        map.AppUserId = _userId;
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var user = await _user.Users.Include(a => a.Company).SingleOrDefaultAsync(a => a.Id == _userId);
        if (user == null) throw new NotFoundException<AppUser>();

        if (id < 0) throw new IdIsNegativeException<Company>();
        var entity = await _repo.FindByIdAsync(id, "AppUser");
        if (entity == null) throw new NotFoundException<Company>();

        if (entity.AppUserId != _userId) throw new UserNotSameException();

        if (entity.Logo != null) _file.Delete(entity.Logo);
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<CompanyListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll == true)
        {
            return _map.Map<ICollection<CompanyListItemDto>>(_repo.GetAllAsync(
                "CompanyIndustries", "CompanyIndustries.Industry", "AppUser", "Advertisments"));
        }
        else
        {
            return _map.Map<ICollection<CompanyListItemDto>>(_repo.FindAllAsync(c => c.IsDeleted == false,
                "CompanyIndustries", "CompanyIndustries.Industry", "AppUser", "Advertisments"));
        }
    }

    public async Task<CompanyDetailItemDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id < 0) throw new IdIsNegativeException<Company>();

        if (takeAll == true)
        {
            var entity = await _repo.FindByIdAsync(id, "CompanyIndustries", "CompanyIndustries.Industry", "AppUser", "Advertisments");
            if (entity == null) throw new NotFoundException<Company>();
            return _map.Map<CompanyDetailItemDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(c => c.Id == id && c.IsDeleted == false,
                "CompanyIndustries", "CompanyIndustries.Industry", "AppUser", "Advertisments");
            if (entity == null) throw new NotFoundException<Company>();
            return _map.Map<CompanyDetailItemDto>(entity);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Company>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Company>();

        entity.IsDeleted = false;
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Company>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Company>();

        entity.IsDeleted = true;
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, CompanyUpdateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var user = await _user.Users.Include(a => a.Company).SingleOrDefaultAsync(a => a.Id == _userId);
        if (user == null) throw new NotFoundException<AppUser>();

        if (id < 0) throw new IdIsNegativeException<Company>();
        var entity = await _repo.FindByIdAsync(id, "CompanyIndustries", "CompanyIndustries.Industry", "AppUser");
        if (entity == null) throw new NotFoundException<Company>();

        if (await _repo.IsExistAsync(c => c.Name == dto.Name && c.Id != id)) throw new IsExistException<Company>();

        if (entity.AppUserId != _userId) throw new UserNotSameException();

        var map = _map.Map(dto, entity);

        if (dto.LogoFIle != null)
        {
            if (entity.Logo != null) _file.Delete(entity.Logo);
            if (!dto.LogoFIle.IsTypeValid("image")) throw new TypeNotValidException();
            if (!dto.LogoFIle.IsSizeValid(2)) throw new SizeNotValidException();
            entity.Logo = await _file.UploadAsync(dto.LogoFIle, RootContsant.CompanyLogoRoot);
        }

        List<CompanyIndustry> ci = new List<CompanyIndustry> { };
        if (dto.IndustryIds != null)
        {
            entity.CompanyIndustries.Clear();

            foreach (var item in dto.IndustryIds)
            {
                var ind = await _industry.FindByIdAsync(item);
                if (ind == null) throw new NotFoundException<Industry>();
                ci.Add(new CompanyIndustry
                {
                    Industry = ind
                });
            }
        }
        map.CompanyIndustries = ci;
        await _repo.SaveAsync();
    }
}
