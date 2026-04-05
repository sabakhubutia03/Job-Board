using Application.DTOs;
using FluentValidation;

namespace Application.Validator;

public class CompanyUpdateValidator : AbstractValidator<CompanuUpdateDto>
{
    public CompanyUpdateValidator()
    {
        RuleFor(n => n.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        RuleFor(n => n.Industry)
            .MaximumLength(100).WithMessage("Industry must not exceed 100 characters");
        RuleFor(n => n.Industry)
            .MaximumLength(100).WithMessage("Industry must not exceed 100 characters");
    }
}