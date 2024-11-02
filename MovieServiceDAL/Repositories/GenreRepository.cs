using Dapper;
using MovieServiceDAL.Entities;
using MovieServiceDAL.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace MovieServiceDAL.Repositories
{
    public class GenreRepository  : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Genres", "Genre")
        {
        }

        public async Task<IEnumerable<object>> GetGenresWithMostMovies()
        {
            string sql = @"SELECT TOP 5 Genres.Name, Count(MoviesGenres.Movie_Id) AS MoviesCount FROM MoviesGenres
                           INNER JOIN Genres ON MoviesGenres.Genre_Id = Genres.Id
                           GROUP BY Genres.Name
                           Order By MoviesCount DESC";

            var result = await _sqlConnection.QueryAsync<Object>(sql, transaction: _dbTransaction);
            var Top5Genres = result.ToList();
            return result;
        }
    }
}
