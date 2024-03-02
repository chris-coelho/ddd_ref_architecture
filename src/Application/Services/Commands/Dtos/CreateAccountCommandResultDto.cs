using Common.Application;

namespace Application.Services.Commands.Dtos;

public class CreateAccountCommandResultDto : IApplicationCommandResult
{
    public Guid AccountId { get; set; }
    public DateTime CreatedOn { get; set; }
}