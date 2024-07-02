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
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Business.Exceptions.Text;
using MyLifeJob.Business.Dtos.RequirementDtos;
using MyLifeJob.Business.Exceptions.Requirement;
using Microsoft.Extensions.Configuration;

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
    readonly IAbilityRepository _ability;
    readonly ITextService _textService;
    readonly ITextRepository _textRepository;
    readonly IRequirementService _reqService;
    readonly IRequirementRepository _reqRepo;
    readonly IConfiguration _config;

    public AdvertismentService(IAdvertismentRepository repo, IMapper mapper, ICategoryRepository category, IHttpContextAccessor accessor,
        UserManager<AppUser> user, ICompanyRepository company, IAbilityRepository ability, ITextService textService,
        ITextRepository textRepository, IRequirementService reqService, IRequirementRepository reqRepo, IConfiguration config)
    {
        _repo = repo;
        _mapper = mapper;
        _category = category;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
        _company = company;
        _ability = ability;
        _textService = textService;
        _textRepository = textRepository;
        _reqService = reqService;
        _reqRepo = reqRepo;
        _config = config;
    }

    public async Task<ICollection<AdvertismentListItemDto>> AcceptGetall()
    {
        var entity = _repo.FindAllAsync(a => a.IsDeleted == false && a.State == State.Accept, "AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company");
        var map = _mapper.Map<ICollection<AdvertismentListItemDto>>(entity);
        foreach (var item in map)
        {
            item.Company.Logo = _config["Jwt:Issuer"] + "wwwroot/" + item.Company.Logo;
            var cat = await _category.FindByIdAsync(item.CategoryId);
            item.Category = cat.Name;
        }
        return map;
    }
    public async Task<AdvertismentDetailItemDto> GetByIdAsync(bool takeAll, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        if (takeAll)
        {
            var entity = await _repo.FindByIdAsync(id, "AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company");
            if (entity == null) throw new NotFoundException<Advertisment>();
            return _mapper.Map<AdvertismentDetailItemDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false, "AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company");
            if (entity == null) throw new NotFoundException<Advertisment>();
            entity.ViewCount++;
            await _repo.SaveAsync();
            var map = _mapper.Map<AdvertismentDetailItemDto>(entity);
            map.Company.Logo = _config["Jwt:Issuer"] + "wwwroot/" + map.Company.Logo;
            var cat = await _category.FindByIdAsync(map.CategoryId);
            map.Category = cat.Name;
            map.AdverCount = await AdverCountForDate();
            return map;
        }
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
        if (entity == null) throw new NotFoundException<Advertisment>();

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

        ICollection<AdvertismentAbility> abilitys = new List<AdvertismentAbility>();
        if (dto.AbilityIds != null)
        {
            foreach (var item in dto.AbilityIds)
            {
                var ability = await _ability.FindByIdAsync(item);
                if (ability == null) throw new NotFoundException<Ability>();
                abilitys.Add(new AdvertismentAbility
                {
                    Ability = ability
                });
            }
        }
        map.AdvertismentAbilities = abilitys;
        var cat = await _category.FindByIdAsync(dto.CategoryId);
        if (cat == null || cat.IsDeleted) throw new NotFoundException<Category>();
        map.CategoryId = cat.Id;
        map.Status = Status.Actice;
        map.CompanyId = company.Id;
        map.State = State.Pending;

        map.EndTime = DateTime.Now.AddDays(31);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();

        foreach (var item in dto.Text)
        {
            await _textService.CreateAsync(new TextCreateDto { Content = item }, map.Id);
        }
        await _textRepository.SaveAsync();

        foreach (var item in dto.Requirement)
        {
            await _reqService.CreateAsync(new RequirementCreateDto { Content = item }, map.Id);
        }
        await _reqRepo.SaveAsync();
    }

    public async Task CreateRequirementInAdvertismentAsync(List<string> context, int adverId)
    {
        if (adverId < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(adverId);
        if (adver == null) throw new NotFoundException<Advertisment>();

        foreach (var item in context)
        {
            await _reqService.CreateAsync(new RequirementCreateDto { Content = item }, adverId);
        }
    }

    public async Task CreateTextInAdvertismentAsync(List<string> context, int adverId)
    {
        if (adverId < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(adverId);
        if (adver == null) throw new NotFoundException<Advertisment>();

        foreach (var item in context)
        {
            await _textService.CreateAsync(new TextCreateDto { Content = item }, adverId);
        }
        await _textRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Advertisment>();

        _repo.Delete(entity);
        await _repo.SaveAsync();
    }

    public async Task DeleteRequirementInAdvertismetUpdateAsync(List<int> ids, int adverId)
    {
        if (adverId < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(adverId, "Requirements");
        if (adver == null) throw new NotFoundException<Advertisment>();

        foreach (var item in ids)
        {
            var req = await _reqService.GetByIdAsync(item);
            if (req == null) throw new NotFoundException<Requirement>();
            await _reqService.DeleteAsync(req.Id);
        }
    }

    public async Task DeleteTextInAdvertismetUpdateAsync(List<int> ids, int adverId)
    {
        if (adverId < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(adverId, "Texts");
        if (adver == null) throw new NotFoundException<Advertisment>();

        foreach (var item in ids)
        {
            if (item < 0) throw new IdIsNegativeException<Text>();
            var text = await _textService.GetByIdAsync(item);
            if (text == null) throw new NotFoundException<Text>();

            if (text.AdvertismentId != adverId) throw new TextAdverIdNotMatchThisIdException();

            await _textService.DeleteAsync(item);
        }
        await _textRepository.SaveAsync();
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
        {
            var map = _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.GetAllAsync("AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company"));

            foreach (var item in map)
            {
                item.Company.Logo = _config["Jwt:Issuer"] + "wwwroot/" + item.Company.Logo;
            }

            return map;
        }
        else
            return _mapper.Map<ICollection<AdvertismentListItemDto>>(_repo.FindAllAsync(a => a.IsDeleted == false, "AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company"));
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
    public async Task<ICollection<AdvertismentListItemDto>> Filter(FilteredAdvertismentDto filter)
    {
        ICollection<AdvertismentListItemDto>? adver = new List<AdvertismentListItemDto>();
        var filteredData = _repo.FindAllAsync(a => a.IsDeleted == false && a.State == State.Accept, "AdvertismentAbilities", "AdvertismentAbilities.Ability", "Texts", "Requirements", "Company");
        var map = _mapper.Map<ICollection<AdvertismentListItemDto>>(filteredData);
        adver = map;

        if (filter.Sort != null)
        {
            adver = await SortAdver(filter.Sort, adver);
        }
        if (filter.City != null)
        {
            adver = await SortAdverCity(filter.City, adver);
        }
        if (filter.Date != null)
        {
            adver = await SortAdverDate(filter.Date, adver);
        }
        if (filter.SortSalary != null)
        {
            adver = await SortAdverSalary(filter.SortSalary, adver);
        }
        foreach (var item in adver)
        {
            item.Company.Logo = _config["Jwt:Issuer"] + "wwwroot/" + item.Company.Logo;
        }

        return adver;
    }

    public async Task<ICollection<AdvertismentListItemDto>> SortAdver(Sort? sort, ICollection<AdvertismentListItemDto> adver)
    {
        ICollection<AdvertismentListItemDto>? advers = new List<AdvertismentListItemDto>();

        if (sort == Sort.Salary)
        {
            var sortDataSalary = adver.OrderByDescending(a => a.Salary);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(sortDataSalary);
        }
        else if (sort == Sort.Position)
        {
            var sortDataTitle = adver.OrderBy(a => a.Title);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(sortDataTitle);
        }
        else if (sort == Sort.Company)
        {
            var sortDataCompany = adver.OrderBy(a => a.Company.Name);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(sortDataCompany);
        }
        else if (sort == Sort.View)
        {
            var sortDataView = adver.OrderByDescending(a => a.ViewCount);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(sortDataView);
        }

        return advers;
    }
    public async Task<ICollection<AdvertismentListItemDto>> SortAdverSalary(SortSalary? salary, ICollection<AdvertismentListItemDto> adver)
    {
        ICollection<AdvertismentListItemDto>? advers = new List<AdvertismentListItemDto>();

        if (salary == SortSalary.ZeroFiveHundred)
        {
            var dataOne = adver.Where(a => a.Salary > 0 && a.Salary <= 500);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(dataOne);
        }
        else if (salary == SortSalary.FiveHundredOneThousand)
        {
            var dataTwo = adver.Where(a => a.Salary >= 500 && a.Salary <= 1000);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(dataTwo);
        }
        else if (salary == SortSalary.OneThousandTwoousand)
        {
            var dataThree = adver.Where(a => a.Salary >= 1000 && a.Salary <= 2000);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(dataThree);
        }
        else if (salary == SortSalary.TwoThousandFiveThousand)
        {
            var dataFour = adver.Where(a => a.Salary >= 2000 && a.Salary <= 5000);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(dataFour);
        }
        else if (salary == SortSalary.FiveThousand)
        {
            var dataFive = adver.Where(a => a.Salary >= 5000);
            advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(dataFive);
        }

        return advers;
    }

    public async Task<ICollection<AdvertismentListItemDto>> SortAdverCity(string? city, ICollection<AdvertismentListItemDto> adver)
    {
        ICollection<AdvertismentListItemDto>? advers = new List<AdvertismentListItemDto>();
        var data = adver.Where(a => a.City == city);
        advers = _mapper.Map<ICollection<AdvertismentListItemDto>>(data);
        return advers;
    }

    public async Task<ICollection<AdvertismentListItemDto>> SortAdverDate(FilterDate? date, ICollection<AdvertismentListItemDto> adver)
    {
        ICollection<AdvertismentListItemDto>? advers = new List<AdvertismentListItemDto>();
        var filteredData = adver.Where(a => a.CreateDate >= DateTime.Now.AddDays(-(int)date));

        var map = _mapper.Map<ICollection<AdvertismentListItemDto>>(filteredData);
        advers = map;

        return advers;
    }
    public async Task UpdateAsync(int id, AdvertismentUpdateDto dto)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var entity = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false, "AdvertismentAbilities", "AdvertismentAbilities.Ability");
        if (entity == null) throw new NotFoundException<Advertisment>();

        var map = _mapper.Map(dto, entity);

        if (dto.CategoryId != null)
        {
            var cat = await _category.GetSingleAsync(c => c.Id == dto.CategoryId && c.IsDeleted == false);
            if (cat == null) throw new NotFoundException<Category>();
        }
        else
            throw new CategoryIdIsEmptyException();

        ICollection<AdvertismentAbility> adverAbility = new List<AdvertismentAbility>();
        if (dto.Abilityids != null)
        {
            if (entity.AdvertismentAbilities != null) entity.AdvertismentAbilities.Clear();
            foreach (var item in dto.Abilityids)
            {
                var ability = await _ability.FindByIdAsync(item);
                if (ability == null) throw new NotFoundException<Ability>();
                adverAbility.Add(new AdvertismentAbility
                {
                    Ability = ability
                });
            }
        }



        entity.AdvertismentAbilities = adverAbility;
        await _repo.SaveAsync();
    }

    public async Task UpdateRequirementInTheAdvertismentAsync(List<int> ids, List<string> texts, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(id, "Requirements");
        if (adver == null) throw new NotFoundException<Advertisment>();

        for (int i = 0; i < ids.Count; i++)
        {
            var req = await _reqService.GetByIdAsync(ids[i]);
            if (req == null) throw new NotFoundException<Requirement>();

            if (req.AdvertismentId != id) throw new RequirementAdverTismentIdNotMatchThisIdException();
            await _reqService.UpdateAsync(new RequirementUpdateItemDto { Content = texts[i] }, ids[i]);
        }
    }

    public async Task UpdateTextInTheAdvertismentAsync(List<int> ids, List<string> texts, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _repo.FindByIdAsync(id, "Texts");
        if (adver == null) throw new NotFoundException<Advertisment>();

        for (var i = 0; i < ids.Count; i++)
        {
            var text = await _textService.GetByIdAsync(ids[i]);
            if (text == null) throw new NotFoundException<Text>();

            if (text.AdvertismentId != id) throw new TextAdverIdNotMatchThisIdException();

            await _textService.UpdateAsync(new TextUpdateItemDto { Content = texts[i] }, ids[i]);
        }
        await _textRepository.SaveAsync();
    }

    public async Task<AdvertismentCountForDatesDto> AdverCountForDate()
    {
        var adver = _repo.GetAllAsync();
        int day = 0;
        int week = 0;
        int month = 0;
        foreach (var item in adver)
        {
            if (item.CreateDate.Day == DateTime.Now.Day)
            {
                day++;
            }
            if (item.CreateDate >= DateTime.Now.AddDays(-7))
            {
                week++;
            }
            if (item.CreateDate.Month == DateTime.Now.Month)
            {
                month++;
            }
        }
        var res = new AdvertismentCountForDatesDto { Day = day, Week = week, Month = month };
        return res;
    }
}
