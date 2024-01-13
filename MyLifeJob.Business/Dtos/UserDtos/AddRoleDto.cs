using FluentValidation;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record AddRoleDto
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
}
public class AddRoleDtoValidator : AbstractValidator<AddRoleDto>
{
    public AddRoleDtoValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull().WithMessage("UserId not be null")
            .NotEmpty().WithMessage("Userid not be empty");
        RuleFor(u => u.RoleName)
          .NotNull().WithMessage("Role Name not be null")
          .NotEmpty().WithMessage("Role Name not be empty");
    }
}