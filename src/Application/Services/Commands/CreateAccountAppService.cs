using Application.Services.Commands.Dtos;
using Common.Application;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services.Commands;

public class CreateAccountAppService : IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto>
{
    private readonly IAccountRepository _accountRepository;
    
    public CreateAccountAppService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public async Task<CreateAccountCommandResultDto> ProcessAsync(CreateAccountCommandDto command, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (account is null)
        {
            account = new Account(command.Name, command.Email);
            if (account.Invalid)
                throw new InvariantViolationException($"Create account failed to email {command.Email}. " +
                                                      $"Details: {string.Join(';',account.ValidationResult.Errors.Select(x => x.ErrorMessage))}");

            await _accountRepository.SaveOrUpdateAsync(account, cancellationToken);
        }
        
        return new CreateAccountCommandResultDto
        {
            AccountId = account.Id,
            CreatedOn = account.CreatedOn
        };
    }
}