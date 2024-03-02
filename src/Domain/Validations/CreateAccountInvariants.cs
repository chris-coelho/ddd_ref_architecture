using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class CreateAccountInvariants : AbstractValidator<Account>
{
    public CreateAccountInvariants()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email");
    }
}