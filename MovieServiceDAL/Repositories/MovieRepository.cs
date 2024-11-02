using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace MovieServiceDAL.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Movies", "Movie")
        {
        }

        public async Task<IEnumerable<Object>> GetMovieGenres(int MovieId)
        {
            string sql = @"SELECT Movies.Title, Movies.Year, Movies.Duration, Movies.Director, Movies.Description, STRING_AGG(Genres.Name, ', ') AS Genres FROM MoviesGenres
                           INNER JOIN Movies ON MoviesGenres.Movie_Id = Movies.Id
                           INNER JOIN Genres ON MoviesGenres.Genre_Id = Genres.Id
                           WHERE Movies.Id = @Id
                           GROUP BY Movies.Title, Movies.Year, Movies.Duration, Movies.Director, Movies.Description";

            var result = await _sqlConnection.QueryAsync<Object>(sql,
                param: new { Id = MovieId },
                transaction: _dbTransaction); 
            if (result == null)
                throw new KeyNotFoundException($"Movie with id [{MovieId}] could not be found.");

            var MovieGenres = result.ToList();

            return MovieGenres;
        }
    }
}
