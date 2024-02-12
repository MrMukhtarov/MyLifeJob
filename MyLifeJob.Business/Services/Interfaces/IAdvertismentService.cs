using MyLifeJob.Business.Dtos.AdvertismentDtos;
using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Services.Interfaces;

public interface IAdvertismentService
{
    Task<ICollection<AdvertismentListItemDto>> GetAll(bool takeAll);
    Task<AdvertismentDetailItemDto> GetByIdAsync(bool takeAll, int id);
    Task CreateAsync(AdvertismentCreateDto dto);
    Task UpdateAsync(int id, AdvertismentUpdateDto dto);
    Task CheckStatus();
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
    Task DeleteAsync(int id);
    Task ExpiresDeletion();
    Task AcceptState(int id);
    Task RejectState(int id);
    Task<ICollection<AdvertismentListItemDto>> AcceptGetall();
    Task ChangeState(int id, State state);
}
