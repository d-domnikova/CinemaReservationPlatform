using ReservationServiceDAL.Interfaces;
using ReservationServiceDAL.Interfaces.Repositories;

namespace ReservationServiceDAL.Data
{
    public class UnitOfWorkEF : IUnitOfWorkEF
    {
        protected readonly ReservationServiceContext dbContext;
        public IReservationRepository ReservationRepository { get; }
        public ITicketTypeRepository TicketTypeRepository { get; }

        public UnitOfWorkEF(ReservationServiceContext dbContext, IReservationRepository reservationRepository, ITicketTypeRepository ticketTypeRepository)
        {
            this.dbContext = dbContext;
            ReservationRepository = reservationRepository;
            TicketTypeRepository = ticketTypeRepository;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
