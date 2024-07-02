using FluentValidation;

namespace MyLifeJob.Business.Dtos.TextDtos;

public record TextUpdateItemDto
{
    public string Content { get; set; }
}
public class TextUpdateItemDtoValidator : AbstractValidator<TextUpdateItemDto>
{
    public TextUpdateItemDtoValidator()
    {
        RuleFor(t => t.Content).NotNull().WithMessage("Text content not be null").NotEmpty().WithMessage("Text Content not be empty");

    }
}
