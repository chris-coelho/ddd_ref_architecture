using Domain.Models;
using Domain.Repositories;

namespace Infra.DataAccess.Repositories;

public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(IUnitOfWorkDomain unitOfWork) : base(unitOfWork) {}
}