using System.Net;
using Application.Controllers.Dtos;
using Application.Controllers.Mappers;
using Application.Services.Commands.Dtos;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/v1/account")]
public class CreateAccountController : ControllerBase
{
    private readonly IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto> _service;

    public CreateAccountController(IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountResponseDto), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.ProcessAsync(request.MapToCreateAccountCommand(), cancellationToken);
            return Ok(result.MapToCreateAccountResponse());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}