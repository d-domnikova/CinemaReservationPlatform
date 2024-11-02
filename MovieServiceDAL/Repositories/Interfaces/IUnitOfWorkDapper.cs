namespace MovieServiceDAL.Repositories.Interfaces
{
    public interface IUnitOfWorkDapper : IDisposable
    {
        IMovieRepository _movieRepository { get; }
        IGenreRepository _genreRepository { get; }
        IShowtimeRepository _showTimeRepository { get; }
        void Commit();
        void Dispose();
    }
}
