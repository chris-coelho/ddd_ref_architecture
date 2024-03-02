using Domain.Models;

namespace Domain.Repositories;

public interface IAccountRepository
{
    Task SaveOrUpdateAsync(Account aggregate, CancellationToken cancellationToken = default);
    Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}