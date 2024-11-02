using MovieServiceDAL.Repositories.Interfaces;
using System.Data;

namespace MovieServiceDAL.Repositories
{
    public class UnitOfWork : IUnitOfWorkDapper, IDisposable
    {
        public IMovieRepository _movieRepository { get; }
        public IGenreRepository _genreRepository { get; }
        public IShowtimeRepository _showTimeRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(IMovieRepository movieRepository, IGenreRepository genreRepository, IShowtimeRepository showTimeRepository, IDbTransaction dbTransaction)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _showTimeRepository = showTimeRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
