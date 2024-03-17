using Application.Services.Commands.Dtos;
using Common.Application;
using Common.Application.NotificationPattern;
using Domain.Models;
using Domain.Repositories;
using Newtonsoft.Json;

namespace Application.Services.Commands;

public class CreateAccountAppService : IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto>
{
    private readonly INotificationContext _notification;
    private readonly IAccountRepository _accountRepository;
    
    public CreateAccountAppService(
        IAccountRepository accountRepository, 
        INotificationContext notification)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task<CreateAccountCommandResultDto?> ProcessAsync(CreateAccountCommandDto command, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (account != null!)
        {
            _notification.AddAsAppService($"An account with email {command.Email} already exists!", 
                $"An account with email {command.Email} already exists!. Payload: {JsonConvert.SerializeObject(command)}");
            return null;
        }
            
        account = new Account(command.Name, command.Email);
        if (account.Invalid)
        {
            _notification.AddAsDomainValidation("Create account failure", account.ValidationResult);
            return null;
        }
        
        await _accountRepository.SaveOrUpdateAsync(account, cancellationToken);
        
        return new CreateAccountCommandResultDto
        {
            AccountId = account.Id,
            CreatedOn = account.CreatedOn
        };
    }
}