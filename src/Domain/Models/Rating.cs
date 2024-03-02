using Common.Domain;

namespace Domain.Models;

public class Rating : ValueObject<Rating>
{
    public virtual double Rate { get; protected set; }
    public virtual long Count { get; protected set; }

    protected Rating() {}
    
    public Rating(double rate, long count)
    {
        Rate = rate;
        Count = count;
    }
    
    protected override IEnumerable<object> AttributesToEqualityCheck()
    {
        return new object[] {Rate, Count};
    }
}