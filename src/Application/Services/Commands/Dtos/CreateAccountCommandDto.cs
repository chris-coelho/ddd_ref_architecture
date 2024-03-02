using Common.Application;

namespace Application.Services.Commands.Dtos;

public class CreateAccountCommandDto : IApplicationCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
}