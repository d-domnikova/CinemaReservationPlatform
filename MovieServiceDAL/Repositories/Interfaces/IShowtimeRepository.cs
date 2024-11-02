using MovieServiceDAL.Entities;

namespace MovieServiceDAL.Repositories.Interfaces
{
    public interface IShowtimeRepository : IGenericRepository<Showtime>
    {
        Task<IEnumerable<Object>> GetMovieInfo();
    }
}

