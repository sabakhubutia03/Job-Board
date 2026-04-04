using Application.DTOs;
using Domain.Entities;
using FluentValidation;

namespace Application.Validator;

public class UserCreateValidator : AbstractValidator<UserCreate>
{
    public UserCreateValidator()
    { 
        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address");

        RuleFor(e => e.Password)
            .NotEmpty().WithMessage("Please enter a password")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");

        RuleFor(n => n.FirstName)
            .NotEmpty().WithMessage("Please enter a first name");
        
        RuleFor(n => n.LastName)
            .NotEmpty().WithMessage("Please enter a last name");
    }
}