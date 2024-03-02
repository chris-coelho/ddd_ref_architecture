using Domain.Models;

namespace Domain.Repositories;

public interface IMovieRepository
{
    Task SaveOrUpdateAsync(Movie aggregate, CancellationToken cancellationToken = default);
    Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}