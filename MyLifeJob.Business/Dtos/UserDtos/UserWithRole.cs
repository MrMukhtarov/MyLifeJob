using MyLifeJob.Business.Dtos.CompanyDtos;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record UserWithRole
{
    public SingleUserItemDto User { get; set; }
    public ICollection<string> Roles { get; set; }
}
