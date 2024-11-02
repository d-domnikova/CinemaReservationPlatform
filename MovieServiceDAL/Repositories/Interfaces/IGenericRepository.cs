namespace MovieServiceDAL.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T t);
        Task<int> AddRangeAsync(IEnumerable<T> list);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
    }
}
