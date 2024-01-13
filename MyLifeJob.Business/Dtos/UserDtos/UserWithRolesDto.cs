namespace MyLifeJob.Business.Dtos.UserDtos;

public record UserWithRolesDto
{
    public ListItemUserDto User { get; set; }
    public ICollection<string> Roles { get; set; }
}
