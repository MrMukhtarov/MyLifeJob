namespace MyLifeJob.Business.Dtos.UserDtos;

public record UserForCompanyDtos
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
