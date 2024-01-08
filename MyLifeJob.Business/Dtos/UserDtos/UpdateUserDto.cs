using FluentValidation;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record UpdateUserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(u => u.Name)
     .NotEmpty()
     .WithMessage("Name not be empty")
     .NotNull()
     .WithMessage("Name not be null")
     .MinimumLength(2)
     .WithMessage("Name must be grather than 2");
        RuleFor(u => u.Surname)
            .NotEmpty()
            .WithMessage("Surname not be empty")
            .NotNull()
            .WithMessage("Surname not be null")
            .MinimumLength(2)
            .WithMessage("Surname must be grather than 2");
        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("Username not be empty")
            .NotNull()
            .WithMessage("Username not be null")
            .MinimumLength(2)
            .WithMessage("Username must be grather than 2");
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email not be empty")
            .NotNull()
            .WithMessage("Email not be null")
            .Matches("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$")
            .WithMessage("Please enter valid email adress");
    }
}
