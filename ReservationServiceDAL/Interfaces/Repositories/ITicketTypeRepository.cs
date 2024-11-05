using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceDAL.Interfaces.Repositories
{
    public interface ITicketTypeRepository : IGenericRepositoryEF<TicketType>
    {
        Task<PagedList<TicketType>> GetAsync(TicketTypeParameters parameters);
    }
}
