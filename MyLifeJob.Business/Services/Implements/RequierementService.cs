using AutoMapper;
using MyLifeJob.Business.Dtos.RequirementDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.Business.Services.Implements;

public class RequierementService : IRequirementService
{
    readonly IRequirementRepository _repo;
    readonly IMapper _mapper;
    readonly IAdvertismentRepository _advertismentRepository;

    public RequierementService(IRequirementRepository repo, IMapper mapper, IAdvertismentRepository advertismentRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _advertismentRepository = advertismentRepository;
    }

    public async Task CreateAsync(RequirementCreateDto dto, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Advertisment>();
        var adver = await _advertismentRepository.FindByIdAsync(id);
        if (adver == null) throw new NotFoundException<Advertisment>();

        var map = _mapper.Map<Requirement>(dto);
        map.AdvertismentId = id;
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Requirement>();
        var req = await _repo.FindByIdAsync(id);
        if (req == null) throw new NotFoundException<Requirement>();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<RequirementListItemDto>> GetAllAsync()
    {
        return _mapper.Map<ICollection<RequirementListItemDto>>(_repo.GetAllAsync("Advertisment"));
    }

    public async Task<RequirementDetailItemDto> GetByIdAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Requirement>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Requirement>();

        return _mapper.Map<RequirementDetailItemDto>(entity);
    }

    public async Task UpdateAsync(RequirementUpdateItemDto dto, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Requirement>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Requirement>();

        var map = _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
