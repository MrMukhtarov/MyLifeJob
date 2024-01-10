using FluentValidation;

namespace MyLifeJob.Business.Dtos.UserDtos;

public record UpdateUserPasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
public class UpdateUserPasswordDtoValidator : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordDtoValidator()
    {
        RuleFor(u => u.CurrentPassword)
            .NotEmpty().WithMessage("Current password not be empty")
            .NotNull().WithMessage("Current password not be Null")
            .MinimumLength(6).WithMessage("Current password must be grather than 6");
        RuleFor(u => u.NewPassword)
           .NotEmpty().WithMessage("New Password not be empty")
           .NotNull().WithMessage("New Password not be Null")
           .MinimumLength(6).WithMessage("New Password must be grather than 6");
        RuleFor(u => u.ConfirmNewPassword)
            .Equal(u => u.NewPassword).WithMessage("Confirm Password do not match new password");
    }
}