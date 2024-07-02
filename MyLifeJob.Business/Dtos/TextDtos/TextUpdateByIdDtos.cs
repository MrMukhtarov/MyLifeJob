using FluentValidation;

namespace MyLifeJob.Business.Dtos.TextDtos;

public record TextUpdateByIdDtos
{
    public int Id { get; set; }
    public string Content { get; set; }
}
public class TextUpdateDtoValidator : AbstractValidator<TextUpdateByIdDtos>
{
    public TextUpdateDtoValidator()
    {
        RuleFor(a => a.Content).NotNull().WithMessage("Text Content not be null")
                                .NotEmpty().WithMessage("Text content not be empty");
    }
}

