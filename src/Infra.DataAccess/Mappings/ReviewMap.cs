using Domain.Models;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infra.DataAccess.Mappings;

public class ReviewMap : ClassMap<Review>
{
    public ReviewMap()
    {
        Table("reviews");

        Id(x => x.Id)
            .GeneratedBy.Assigned();

        Version(x => x.ModifiedAt)
            .Column("modified_at")
            .CustomType<UtcDateTimeType>();
        
        Map(x => x.Rate)
            .Column("rate")
            .Not.Nullable();

        Map(x => x.Comments)
            .Column("comments")
            .CustomSqlType("text");

        Map(x => x.AccountId)
            .Column("account_id")
            .Not.Nullable();

        Map(x => x.OccurredOn)
            .Column("occurred_on")
            .Not.Nullable();
        
        References(x => x.Movie, "movie_id");
    }
}