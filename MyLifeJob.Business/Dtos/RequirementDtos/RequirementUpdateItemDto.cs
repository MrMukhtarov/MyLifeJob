using FluentValidation;

namespace MyLifeJob.Business.Dtos.RequirementDtos;

public record RequirementUpdateItemDto
{
    public string Content { get; set; }
}
public class RequirementUpdateItemDtoValidator : AbstractValidator<RequirementUpdateItemDto>
{
    public RequirementUpdateItemDtoValidator()
    {
        RuleFor(r => r.Content).NotEmpty().WithMessage("Advertisment Requeirement not be empty")
                               .NotNull().WithMessage("Advertisment Requeirement not be null");
    }
}