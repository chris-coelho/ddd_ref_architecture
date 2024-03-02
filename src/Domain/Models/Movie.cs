using Common.Domain;

namespace Domain.Models;

public class Movie : Aggregate
{
    protected ICollection<Review> ReviewList { get; set; }
    public virtual string Name { get; protected set; }
    public virtual int Year { get; protected set; }
    public virtual Genre Genre { get; protected set; }
    public virtual Rating Rating { get; protected set; }

    public virtual IReadOnlyCollection<Review> Reviews => ReviewList.ToList();

    protected Movie() {}

    public Movie(string name, int year, Genre genre, Guid id = default)
    {
        Id = id;
        Name = name;
        Year = year;
        Genre = genre;
    }
    
}