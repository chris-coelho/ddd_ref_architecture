using Application.Services.Commands;
using Application.Services.Commands.Dtos;
using Common.Application;

namespace Application.DI;

public static class ApplicationLayerExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        #region Commands
        services.AddScoped<IApplicationCommandServiceWithResultAsync<CreateAccountCommandDto, CreateAccountCommandResultDto>, 
            CreateAccountAppService>();
        #endregion

        #region Queries
        //services.AddScoped<ITabelaFipeRepository, TabelaFipeRepository>();
        #endregion

        return services;
    }
}