using FluentValidation;

namespace MyLifeJob.Business.Dtos.RequirementDtos;

public record RequirementCreateDto
{
    public string Content { get; set; }
}
public class RequirementCreateDtoValidator : AbstractValidator<RequirementCreateDto>
{
    public RequirementCreateDtoValidator()
    {
        RuleFor(r => r.Content).NotEmpty().WithMessage("Advertisment Requeirement not be empty")
                               .NotNull().WithMessage("Advertisment Requeirement not be null");
    }
}
