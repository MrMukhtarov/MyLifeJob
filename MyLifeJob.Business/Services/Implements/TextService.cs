using AutoMapper;
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Business.Exceptions.Commons;
using MyLifeJob.Business.Services.Interfaces;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Repositories.Interfaces;

namespace MyLifeJob.Business.Services.Implements;

public class TextService : ITextService
{
    readonly ITextRepository _repo;
    readonly IMapper _mapper;
    readonly IAdvertismentRepository _advertismentRepository;

    public TextService(ITextRepository repo, IMapper mapper, IAdvertismentRepository advertismentRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _advertismentRepository = advertismentRepository;
    }

    public async Task CreateAsync(TextCreateDto dto, int AdverId)
    {
        var adver = await _advertismentRepository.FindByIdAsync(AdverId);
        if (adver == null) throw new NotFoundException<Advertisment>();

        var map = _mapper.Map<Text>(dto);
        map.AdvertismentId = AdverId;
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Text>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Text>();
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<TextListItemDtos>> GetAllAsync()
    {
        return _mapper.Map<ICollection<TextListItemDtos>>(_repo.GetAllAsync());
    }

    public async Task<TextDetailItemDto> GetByIdAsync(int id)
    {
        if (id < 0) throw new IdIsNegativeException<Text>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Text>();
        return _mapper.Map<TextDetailItemDto>(entity);
    }

    public async Task UpdateAsync(TextUpdateItemDto dto, int id)
    {
        if (id < 0) throw new IdIsNegativeException<Text>();
        var entity = await _repo.FindByIdAsync(id);
        if (entity == null) throw new NotFoundException<Text>();
        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
    public async Task UpdateByIdAsync(TextUpdateByIdDtos dto)
    {
        if (dto.Id < 0) throw new IdIsNegativeException<Text>();
        var entity = await _repo.FindByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException<Text>();
        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
