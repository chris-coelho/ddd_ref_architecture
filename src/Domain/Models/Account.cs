using Common.Domain;
using Common.Util;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Models;

public class Account : Aggregate
{
    public virtual string Name { get; protected set; }
    public virtual string Email { get; protected set; }
    public virtual DateTime CreatedOn { get; protected set; }

    protected Account() {}
    
    public Account(string name, string email, Guid id = default)
    {
        Id = id;
        Name = name;
        Email = email;
        CreatedOn = DateTime.Now.GetBrazilianTime();

        CheckInvariants(this, new CreateAccountInvariants(), new List<ValidationResult>());
    }

}