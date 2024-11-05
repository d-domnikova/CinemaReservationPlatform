using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceDAL.Interfaces.Repositories
{
    public interface IReservationRepository : IGenericRepositoryEF<Reservation>
    {
        Task<PagedList<Reservation>> GetAsync(ReservationParameters parameters);
    }
}
