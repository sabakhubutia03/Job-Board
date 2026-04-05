using Application.DTOs;
using FluentValidation;

namespace Application.Validator;

public class JobUpdateValidator : AbstractValidator<JobUpdateDto>
{
    public JobUpdateValidator()
    {
        RuleFor(t => t.Title)
            .MaximumLength(100).WithMessage("Title max length exceeded");
        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("Description max length exceeded");
        RuleFor(i => i.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId must be greater than 0")
            .When(x => x.CompanyId.HasValue);
    }
}