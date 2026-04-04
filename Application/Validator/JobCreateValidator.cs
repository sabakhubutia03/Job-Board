using Application.DTOs;
using FluentValidation;

namespace Application.Validator;

public class JobCreateValidator : AbstractValidator<JobCreateDto>
{
    public JobCreateValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
        
        RuleFor(c => c.CompanyId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("CompanyId must be greater than zero");
    }
}