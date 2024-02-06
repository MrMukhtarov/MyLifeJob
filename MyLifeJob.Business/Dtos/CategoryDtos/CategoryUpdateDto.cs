using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Validators;

namespace MyLifeJob.Business.Dtos.CategoryDtos;

public record CategoryUpdateDto
{
    public string Name { get; set; }
    public IFormFile? LogoFile { get; set; }
}
public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Category name not be empty")
                            .NotNull().WithMessage("Categiry name not be null")
                            .MinimumLength(1).WithMessage("Categiry name must be graher than 1");
        RuleFor(c => c.LogoFile).SetValidator(new FileValidator());
    }
}
