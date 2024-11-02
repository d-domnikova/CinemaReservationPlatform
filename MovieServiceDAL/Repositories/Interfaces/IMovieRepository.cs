using MovieServiceDAL.Entities;

namespace MovieServiceDAL.Repositories.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Object>> GetMovieGenres(int MovieId);
    }
}
