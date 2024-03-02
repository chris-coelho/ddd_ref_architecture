using Domain.Models;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infra.DataAccess.Mappings;

public class AccountMap : ClassMap<Account>
{
    public AccountMap()
    {
        Table("accounts");

        Id(x => x.Id)
            .GeneratedBy.Assigned();

        Version(x => x.ModifiedAt)
            .Column("modified_at")
            .CustomType<UtcDateTimeType>();

        Map(x => x.Name)
            .Column("name")
            .Not.Nullable();

        Map(x => x.Email)
            .Column("email")
            .Not.Nullable()
            .UniqueKey("uk_account_email");
        
        Map(x => x.CreatedOn)
            .Column("created_on")
            .Not.Nullable();
    }
}