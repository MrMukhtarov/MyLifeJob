using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Validators;

namespace MyLifeJob.Business.Dtos.CategoryDtos;

public record CategoryCreateDto
{
    public string Name { get; set; }
    public IFormFile LogoFile { get; set; }
}
public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Category name not be empty")
                            .NotNull().WithMessage("Categiry name not be null")
                            .MinimumLength(1).WithMessage("Categiry name must be graher than 1");
        RuleFor(c => c.LogoFile).NotEmpty().WithMessage("Category logo not be empty")
                                .NotNull().WithMessage("Categiry logo not be null")
                                .SetValidator(new FileValidator());
    }
}
