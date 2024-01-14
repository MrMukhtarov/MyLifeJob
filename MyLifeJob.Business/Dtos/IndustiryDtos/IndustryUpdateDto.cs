using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Validators;

namespace MyLifeJob.Business.Dtos.IndustiryDtos;

public record IndustryUpdateDto
{
    public string Name { get; set; }
    public IFormFile? LogoFile { get; set; }
}
public class IndustryUpdateDtoValidator : AbstractValidator<IndustryUpdateDto>
{
    public IndustryUpdateDtoValidator()
    {
        RuleFor(i => i.Name)
          .NotEmpty().WithMessage("Industiry name not be empty")
          .NotNull().WithMessage("Indistiry name not be null")
          .MinimumLength(2).WithMessage("Industiry name must be grather than 2");
        RuleFor(i => i.LogoFile)
            .SetValidator(new FileValidator());
    }
}
