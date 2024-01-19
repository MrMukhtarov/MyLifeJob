using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Validators;

namespace MyLifeJob.Business.Dtos.CompanyDtos;

public record CompanyUpdateDto
{
    public string Name { get; set; }
    public IFormFile? LogoFIle { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string? Website { get; set; }
    public string Description { get; set; }
    public List<int>? IndustryIds { get; set; }
}
public class CompanyUpdateDtoValidator : AbstractValidator<CompanyUpdateDto>
{
    public CompanyUpdateDtoValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Company Name not be null")
    .NotEmpty().WithMessage("Company name not be empty")
    .MinimumLength(2).WithMessage("Companys name must be grather than 2");
        RuleFor(c => c.LogoFIle).SetValidator(new FileValidator());
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email not be empty")
            .NotNull().WithMessage("Email not be null")
            .Matches("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$")
            .WithMessage("Please enter valid email adress");
        RuleFor(c => c.Phone).NotNull().WithMessage("Phone number not be null")
            .NotEmpty().WithMessage("Phone number not be empty");
        RuleFor(c => c.Location).NotNull().WithMessage("Location not be null")
            .NotEmpty().WithMessage("Location not be empty");
        RuleFor(c => c.Website)
            .Matches(@"^(?:(?:(?:ftp|http|https):\/\/)?(?:www\.)?)?(?!(?:ftp|http|https|www\.))[a-zA-Z0-9_-]+(?:\.[a-zA-Z]+)+(?:\/[\w#]+)*(?:\/\w+\?[a-zA-Z0-9_]+=\\w+(&[a-zA-Z0-9_]+=\\w+)*)?\/?$")
            .WithMessage("Please enter a valid website")
            .When(c => c.Website != null);
        RuleFor(c => c.Description).NotNull().WithMessage("Company Description not be null")
            .NotEmpty().WithMessage("Company Description not be empty");
        RuleFor(c => c.IndustryIds).Must(c => CheckSameId(c)).WithMessage("You can not and same industry");
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