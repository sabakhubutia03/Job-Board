using Application.DTOs;
using FluentValidation;

namespace Application.Validator;

public class CompanyCreateValidator : AbstractValidator<CompanyCreateDto>
{
    public CompanyCreateValidator()
    {
        RuleFor(n => n.Name) 
            .NotEmpty()
            .WithMessage("Company name is required");
        RuleFor(I => I.Industry)
            .NotEmpty()
            .WithMessage("Industry type is required"); 
    }
}