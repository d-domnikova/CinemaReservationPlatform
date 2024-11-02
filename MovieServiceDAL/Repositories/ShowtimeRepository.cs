using System.Data.SqlClient;
using System.Data;
using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;
using Dapper;

namespace MovieServiceDAL.Repositories
{
    public class ShowtimeRepository : GenericRepository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Showtimes", "Showtime")
        {
        }

        public async Task<IEnumerable<Object>> GetMovieInfo()
        {
            string sql = @"SELECT * FROM Showtimes
                           INNER JOIN Movies ON Movies.Id = Showtimes.MovieId";

            var result = await _sqlConnection.QueryAsync<Object>(sql, transaction: _dbTransaction);
            var MovieInfo = result.ToList();
            return result;
        }
    }
}
