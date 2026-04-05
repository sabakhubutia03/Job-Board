using Application.DTOs;
using FluentValidation;

namespace Application.Validator;

public class UserUpdateValidator : AbstractValidator<UserUpdate>
{
    public UserUpdateValidator()
    {
        RuleFor(n => n.FirstName)
            .MaximumLength(50); 
        RuleFor(l => l.LastName)
            .MaximumLength(50);
        RuleFor(n => n.Email)
            .EmailAddress().WithMessage("Invalid email address");
    }
}