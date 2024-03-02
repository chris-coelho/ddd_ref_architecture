using Common.Domain;
using Common.Util;

namespace Domain.Models;

public class Review : Entity
{
    public virtual double Rate { get; protected set; }
    public virtual string Comments { get; protected set; }
    public virtual Guid AccountId { get; protected set; }
    public virtual DateTime OccurredOn { get; protected set; }
    public virtual Movie Movie { get; protected set; }

    protected Review() {}

    public Review(Movie movie, double rate, string comments, Guid accountId, Guid id = default)
    {
        Id = id;
        Movie = movie;
        Rate = rate;
        Comments = comments;
        AccountId = accountId;
        OccurredOn = DateTime.Now.GetBrazilianTime();
    }

}