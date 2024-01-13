using FluentValidation;

namespace MyLifeJob.Business.Dtos.RoleDtos;

public record UpdateRoleDto
{
    public string Name { get; set; }
    public string NewName { get; set; }
}
public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleDtoValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage("Role Name not be null")
            .NotEmpty().WithMessage("Role Name not be empty");
        RuleFor(r => r.NewName)
           .NotNull().WithMessage("Role New Name not be null")
           .NotEmpty().WithMessage("Role New Name not be empty");
    }
}
