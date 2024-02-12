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
using MyLifeJob.Business.Exceptions.Advertisment;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.Business.Services.Implements;

public class AdvertismentService : IAdvertismentService
{
    readonly IAdvertismentRepository _repo;
    readonly IMapper _mapper;
    readonly ICategoryRepository _category;
    readonly string? _userId;
    readonly IHttpContextAccessor _accessor;
    readonly UserManager<AppUser> _user;
    readonly ICompanyRepository _company;

    public AdvertismentService(IAdvertismentRepository repo, IMapper mapper, ICategoryRepository category, IHttpContextAccessor accessor,
        UserManager<AppUser> user, ICompanyRepository company)
    {
        _repo = repo;
        _mapper = mapper;
        _category = category;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
        _company = company;
    }

    public async Task<ICollection<AdvertismentListItemDto>> AcceptGetall()
    {
        var entity = _repo.FindAllAsync(a => a.IsDeleted == false && a.State == State.Accept);
        return _mapper.Map<ICollection<AdvertismentListItemDto>>(entity);
    }

    public async Task AcceptState(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var adv = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false && a.State == State.Pending);
        if (adv == null) throw new NotFoundException<Advertisment>();

        adv.State = State.Accept;
        await _repo.SaveAsync();
    }

    public async Task ChangeState(int id, State state)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.FindByIdAsync(id);
        if(entity == null) throw new NotFoundException<Advertisment>();

        entity.State = state;
        await _repo.SaveAsync();
    }

    public async Task CheckStatus()
    {
        var adv = _repo.FindAllAsync(a => a.IsDeleted == false);
        foreach (var item in adv)
        {
            if (item.EndTime < DateTime.Now)
            {
                item.Status = Status.Finished;
            }
        }
        await _repo.SaveAsync();
    }

    public async Task CreateAsync(AdvertismentCreateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new NullReferenceException();
        var user = await _user.Users.Include(u => u.Company).FirstOrDefaultAsync(a => a.Id == _userId);
        if (user == null) throw new NotFoundException<AppUser>();

        if (user.Company == null) throw new UserHaventCompanyException();
        var company = await _company.FindByIdAsync(user.Company.Id);
        if (company == null) throw new NotFoundException<Company>();

        var map = _mapper.Map<Advertisment>(dto);

        var cat = await _category.FindByIdAsync(dto.CategoryId);
        if (cat == null || cat.IsDeleted) throw new NotFoundException<Category>();
        map.CategoryId = cat.Id;
        map.Status = Status.Actice;
        map.CompanyId = company.Id;
        map.State = State.Pending;

        map.EndTime = DateTime.Now.AddDays(31);

        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Advertisment>();

        _repo.Delete(entity);
        await _repo.SaveAsync();
    }

    public async Task ExpiresDeletion()
    {
        var entitys = _repo.FindAllAsync(a => a.IsDeleted == false && a.Status == Status.Finished);
        foreach (var i in entitys)
        {
            if (i.EndTime.AddDays(7) <= DateTime.Now)
                _repo.SofDelete(i);
        }
        await _repo.SaveAsync();
    }

    public async Task<ICollection<AdvertismentListItemDto>> GetAll(bool takeAll)
    {
        if (takeAll)
            return _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.GetAllAsync());
        else
            return _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.FindAllAsync(a => a.IsDeleted == false));
    }

    public async Task<AdvertismentDetailItemDto> GetByIdAsync(bool takeAll, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        if (takeAll)
        {
            var entity = await _repo.FindByIdAsync(id);
            if (entity == null) throw new NotFoundException<Advertisment>();
            return _mapper.Map<AdvertismentDetailItemDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false);
            if (entity == null) throw new NotFoundException<Advertisment>();
            entity.ViewCount++;
            await _repo.SaveAsync();
            return _mapper.Map<AdvertismentDetailItemDto>(entity);
        }
    }

    public async Task RejectState(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var adv = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false && a.State == State.Pending);
        if (adv == null) throw new NotFoundException<Advertisment>();

        adv.State = State.Reject;
        await _repo.SaveAsync();
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Advertisment>();

        entity.CreateDate = DateTime.Now;
        entity.EndTime = DateTime.Now.AddDays(31);

        entity.IsDeleted = false;
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Advertisment>();

        entity.CreateDate = DateTime.MinValue;
        entity.EndTime = DateTime.MinValue;

        entity.IsDeleted = true;
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, AdvertismentUpdateDto dto)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false);
        if (entity == null) throw new NotFoundException<Advertisment>();

        var map = _mapper.Map(dto, entity);

        if (dto.CategoryId != null)
        {
            var cat = await _category.GetSingleAsync(c => c.Id == dto.CategoryId && c.IsDeleted == false);
            if (cat == null) throw new NotFoundException<Category>();
        }
        else
            throw new CategoryIdIsEmptyException();

        await _repo.SaveAsync();
    }

}
