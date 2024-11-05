
using ReservationServiceDAL.Interfaces;
using ReservationServiceDAL.Interfaces.Repositories;

namespace ReservationServiceDAL.Data
{
    public class UnitOfWorkEF : IUnitOfWorkEF
    {
        protected readonly ReservationServiceContext dbContext;
        public IReservationRepository ReservationRepository { get; }
        public ITicketTypeRepository TicketTypeRepository { get; }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
