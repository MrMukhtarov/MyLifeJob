using MyLifeJob.Business.Dtos.AdvertismentDtos;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAdvertismentService
{
    Task<ICollection<AdvertismentListItemDto>> GetAll(bool takeAll);
    Task CreateAsync(AdvertismentCreateDto dto);
}
