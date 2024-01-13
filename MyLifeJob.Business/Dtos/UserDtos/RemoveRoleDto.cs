using FluentValidation;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record RemoveRoleDto
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
}

public class RemoveRoleDtoValidator : AbstractValidator<RemoveRoleDto>
{
    public RemoveRoleDtoValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull().WithMessage("UserId not be null")
            .NotEmpty().WithMessage("Userid not be empty");
        RuleFor(u => u.RoleName)
          .NotNull().WithMessage("Role Name not be null")
          .NotEmpty().WithMessage("Role Name not be empty");
    }
}
