using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Dtos.AbilityDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.Business.Services.Implements;

public class AbilityService : IAbilityService
{
    readonly IAbilityRepository _repo;
    readonly IMapper _mapper;

    public AbilityService(IAbilityRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(string Name)
    {
        var exist = await _repo.IsExistAsync(a => a.Name == Name);
        if (exist != false) throw new IsExistException<Ability>();
        Ability ability = new Ability();
        ability.Name = Name;
        await _repo.CreateAsync(ability);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Ability>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Ability>();
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<AbilityListItemDto>> GetAllAsync()
    {
        var entitys = _repo.GetAllAsync();
        return _mapper.Map<ICollection<AbilityListItemDto>>(entitys);
    }

    public async Task<AbilityDetailDto> GetByIdAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Ability>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Ability>();
        return _mapper.Map<AbilityDetailDto>(await _repo.FindByIdAsync(id));
    }
}
