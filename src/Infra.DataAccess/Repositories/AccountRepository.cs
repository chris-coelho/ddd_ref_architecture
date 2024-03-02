using Domain.Models;
using Domain.Repositories;
using NHibernate.Linq;

namespace Infra.DataAccess.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(IUnitOfWorkDomain unitOfWork) : base(unitOfWork) {}

    public async Task<Account> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Session
            .Query<Account>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);
    }
}