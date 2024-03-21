using FluentValidation;

namespace MyLifeJob.Business.Dtos.TextDtos;

public record TextCreateDto
{
    public string Content { get; set; }
    //public int AdvertismentId { get; set; }
}
public class TextCreateDtoValidator : AbstractValidator<TextCreateDto>
{
    public TextCreateDtoValidator()
    {
        RuleFor(t => t.Content).NotNull().WithMessage("Text content not be null").NotEmpty().WithMessage("Text Content not be empty");
    }
}