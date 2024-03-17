using Application.Services.Commands;
using Application.Services.Commands.Dtos;
using Common.Application;
using Common.Application.NotificationPattern;

namespace Application.DI;

public static class ApplicationLayerExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationContext, NotificationContext>();

        #region Commands
        services.AddScoped<IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto>, 
            CreateAccountAppService>();
        #endregion

        #region Queries
        // services.AddScoped<IApplicationQueryServiceAsync<GetAccountDetailsQueryDto, GetAccountDetailsQueryResultDto>, 
        //     GetAccountDetailsAppService>();
        #endregion

        return services;
    }
}