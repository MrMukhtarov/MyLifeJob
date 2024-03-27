using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyLifeJob.Business.Dtos.TextDtos;
using MyLifeJob.Core.Enums;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record AdvertismentCreateDto
{
    public string Title { get; set; }
    public string City { get; set; }
    public decimal? Salary { get; set; }
    public string WorkGraphic { get; set; }
    public List<int>? AbilityIds { get; set; }
    public List<string> Text { get; set; }
    public List<string> Requirement { get; set; }
    public string? Experience { get; set; }
    public Education? Education { get; set; }
    public int CategoryId { get; set; }
}
public class AdvertismentCreateDtoValidator : AbstractValidator<AdvertismentCreateDto>
{
    public AdvertismentCreateDtoValidator()
    {
        RuleFor(a => a.Title).NotEmpty().WithMessage("Advertisment title not be empty").NotNull().WithMessage("Advertisment title not be null")
                             .MinimumLength(1).WithMessage("Advertisment title must be grather than 1");
        RuleFor(a => a.City).NotEmpty().WithMessage("Advertisment city not be empty").NotNull().WithMessage("Advertisment city not be null");
        RuleFor(a => a.Salary).GreaterThan(344).WithMessage("Salary must be grather than 345").When(a => a.Salary != null);
        RuleFor(a => a.WorkGraphic).NotEmpty().WithMessage("Advertisment Work Graphic not be empty").NotNull().WithMessage("Advertisment Work Graphic not be null");
        RuleFor(a => a.AbilityIds).Must(c => CheckSameId(c)).WithMessage("Ability id same").When(a => a.AbilityIds != null);
        RuleFor(a => a.Experience).MinimumLength(20).WithMessage("Experience length must be grather than 20").When(a => a.Experience != null);
        RuleFor(a => a.Education).Must(BeAValidEducation).WithMessage("Education length must be grather than 20").When(a => a.Education != null);
        RuleFor(a => a.CategoryId).NotEmpty().WithMessage("Advertisment Category Id not be empty").NotNull().WithMessage("Advertisment Category Id not be null")
                                  .GreaterThan(0).WithMessage("Category Id must be grather than 0");
    }
    private bool BeAValidEducation(Education? edu)
    {
        return edu.HasValue && Enum.IsDefined(typeof(Education), edu.Value);
    }

    private bool CheckSameId(List<int> ids)
    {
        var encounteredIds = new HashSet<int>();

        if (ids == null || ids.Count == 0)
        {
            return false;
        }

        foreach (var id in ids)
        {
            if (!encounteredIds.Contains(id))
            {
                encounteredIds.Add(id);
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
