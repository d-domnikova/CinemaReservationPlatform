using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Exceptions;
using ReservationServiceDAL.Interfaces.Repositories;

namespace ReservationServiceDAL.Data.Repositories
{
    public abstract class GenericRepositoryEF<TEntity> : IGenericRepositoryEF<TEntity> where TEntity : class
    {
        protected readonly ReservationServiceContext dbContext;
        protected readonly DbSet<TEntity> table;
        public GenericRepositoryEF(ReservationServiceContext dbContext)
        {
            this.dbContext = dbContext;
            table = this.dbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync() => await table.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await table.FindAsync(id)
                ?? throw new EntityNotFoundException(
                    GetEntityNotFoundErrorMessage(id));
        }

        public abstract Task<TEntity> GetCompleteEntityAsync(int id);

        public virtual async Task InsertAsync(TEntity entity) => await table.AddAsync(entity);

        public virtual async Task UpdateAsync(TEntity entity) =>
            await Task.Run(() => table.Update(entity));

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await Task.Run(() => table.Remove(entity));
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(TEntity).Name} with id {id} not found.";
    }
}
