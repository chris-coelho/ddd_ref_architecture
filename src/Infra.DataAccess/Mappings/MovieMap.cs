using Domain.Models;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infra.DataAccess.Mappings;

public class MovieMap : ClassMap<Movie>
{
    public MovieMap()
    {
        Table("movies");

        Id(x => x.Id)
            .GeneratedBy.Assigned();

        Version(x => x.ModifiedAt)
            .Column("modified_at")
            .CustomType<UtcDateTimeType>();

        Map(x => x.Name)
            .Column("name")
            .Not.Nullable();

        Map(x => x.Year)
            .Column("year")
            .Not.Nullable();

        Map(x => x.Genre)
            .Column("genre")
            .Not.Nullable();

        Component(x => x.Rating, y =>
        {
            y.Map(x => x.Rate).Column("rate");
            y.Map(x => x.Count).Column("count");
        });
        
        HasMany<Review>(Reveal.Member<Movie>("ReviewList"))
            .KeyColumn("movie_id")
            .ForeignKeyConstraintName("fk_movie_id")
            .Not.LazyLoad()
            .Cascade.SaveUpdate()
            .Cascade.DeleteOrphan();
    }
}