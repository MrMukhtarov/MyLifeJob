namespace MyLifeJob.Business.Dtos.UserDtos;

public record ListItemUserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string IsDeleted { get; set; }
}
