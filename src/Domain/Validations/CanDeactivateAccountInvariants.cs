using Domain.Models;
using FluentValidation;

namespace Domain.Validations;

public class CanDeactivateAccountInvariants : AbstractValidator<Account>
{
    public CanDeactivateAccountInvariants()
    {
        RuleFor(x => x.Active)
            .Equal(true)
            .WithMessage("You can not deactivate account because it is already deactivate");
    }
}