using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Validators;

namespace MyLifeJob.Business.Dtos.IndustiryDtos;

public class IndustryCreateDto
{
    public string Name { get; set; }
    public IFormFile LogoFile { get; set; }
}
public class IndustryCreateDtoValidator : AbstractValidator<IndustryCreateDto>
{
    public IndustryCreateDtoValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Industiry name not be empty")
            .NotNull().WithMessage("Indistiry name not be null")
            .MinimumLength(2).WithMessage("Industiry name must be grather than 2");
        RuleFor(i => i.LogoFile)
            .NotEmpty().WithMessage("Industiry logo not be empty")
            .NotNull().WithMessage("Indistiry logo not be null")
            .SetValidator(new FileValidator());
    }
}