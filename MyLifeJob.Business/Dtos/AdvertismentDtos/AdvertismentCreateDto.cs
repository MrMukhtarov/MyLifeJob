using FluentValidation;

namespace MyLifeJob.Business.Dtos.AdvertismentDtos;

public record AdvertismentCreateDto
{
    public string Title { get; set; }
    public string City { get; set; }
    public decimal? Salary { get; set; }
    public string WorkGraphic { get; set; }
    public string? Ability { get; set; }
    public string Text { get; set; }
    public string Requirement { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
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
        RuleFor(a => a.Ability).MinimumLength(20).WithMessage("Ability length must be grather than 20").When(a => a.Ability != null);
        RuleFor(a => a.Text).NotEmpty().WithMessage("Advertisment Text not be empty").NotNull().WithMessage("Advertisment Text not be null")
                            .MinimumLength(30).WithMessage("Text length must be grather than 30");
        RuleFor(a => a.Requirement).NotEmpty().WithMessage("Advertisment Requirement not be empty").NotNull().WithMessage("Advertisment Requirement not be null")
                                   .MinimumLength(30).WithMessage("Requirement length must be grather than 30");
        RuleFor(a => a.Experience).MinimumLength(20).WithMessage("Experience length must be grather than 20").When(a => a.Ability != null);
        RuleFor(a => a.Education).MinimumLength(20).WithMessage("Education length must be grather than 20").When(a => a.Ability != null);
        RuleFor(a => a.CategoryId).NotEmpty().WithMessage("Advertisment Category Id not be empty").NotNull().WithMessage("Advertisment Category Id not be null")
                                  .GreaterThan(0).WithMessage("Category Id must be grather than 0");
    }
}
