using FluentValidation;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("UserName not be empty")
            .NotNull()
            .WithMessage("Username not be null");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password not be empty")
            .NotNull()
            .WithMessage("Password not be null")
            .MinimumLength(6)
            .WithMessage("Password must be grather than 6");
    }   
}
