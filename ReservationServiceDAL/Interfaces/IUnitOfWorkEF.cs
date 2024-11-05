using ReservationServiceDAL.Interfaces.Repositories;

namespace ReservationServiceDAL.Interfaces
{
    public interface IUnitOfWorkEF
    {
        IReservationRepository ReservationRepository { get; }
        ITicketTypeRepository TicketTypeRepository { get; }
        Task SaveChangesAsync();
    }
}
