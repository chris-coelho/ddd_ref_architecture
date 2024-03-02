using Domain.Repositories;
using Infra.DataAccess.Repositories;

namespace Application.DI;

public static class DataAccessLayerExtentions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        #region Repositories
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        #endregion

        #region DAO
        //services.AddScoped<ITabelaFipeRepository, TabelaFipeRepository>();
        #endregion

        return services;
    }
}