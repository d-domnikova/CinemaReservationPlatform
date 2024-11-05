namespace ReservationServiceDAL.Interfaces.Repositories
{
    public interface IGenericRepositoryEF<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetCompleteEntityAsync(int id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}
