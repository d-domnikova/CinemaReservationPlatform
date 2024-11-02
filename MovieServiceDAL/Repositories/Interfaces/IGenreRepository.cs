using MovieServiceDAL.Entities;

namespace MovieServiceDAL.Repositories.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IEnumerable<Object>> GetGenresWithMostMovies();
    }
}
