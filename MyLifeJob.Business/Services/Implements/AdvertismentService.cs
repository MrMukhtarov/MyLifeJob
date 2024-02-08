using AutoMapper;
using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;
using MyLifeJob.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace MyLifeJob.Business.Services.Implements;

public class AdvertismentService : IAdvertismentService
{
    readonly IAdvertismentRepository _repo;
    readonly IMapper _mapper;
    readonly ICategoryRepository _category;
    readonly string? _userId;
    readonly IHttpContextAccessor _accessor;
    readonly UserManager<AppUser> _user;

    public AdvertismentService(IAdvertismentRepository repo, IMapper mapper, ICategoryRepository category, IHttpContextAccessor accessor,
        UserManager<AppUser> user)
    {
        _repo = repo;
        _mapper = mapper;
        _category = category;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
    }

    public async Task CreateAsync(AdvertismentCreateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new NullReferenceException();
        var user = await _user.FindByIdAsync(_userId);
        if (user == null) throw new NotFoundException<AppUser>();

        var map = _mapper.Map<Advertisment>(dto);

        var cat = await _category.FindByIdAsync(dto.CategoryId);
        if (cat == null || cat.IsDeleted) throw new NotFoundException<Category>();
        map.CategoryId = cat.Id;
        map.Status = Status.Actice;

        map.EndTime = DateTime.Now.AddDays(31);

        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<AdvertismentListItemDto>> GetAll(bool takeAll)
    {
        if (takeAll)
        {
            return _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.GetAllAsync());
        }
        else
        {
            return _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.FindAllAsync(a => a.IsDeleted == false));
        }
    }
}
    