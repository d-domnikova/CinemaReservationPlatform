using ReservationServiceBLL.DTO.Requests;
using ReservationServiceBLL.DTO.Responses;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceBLL.Interfaces
{
    public interface ITicketTypeService
    {
        Task<IEnumerable<TicketTypeResponse>> GetAsync();

        Task<PagedList<TicketTypeResponse>> GetAsync(TicketTypeParameters parameters);

        Task<TicketTypeResponse> GetByIdAsync(int id);

        Task InsertAsync(TicketTypeRequest request);

        Task UpdateAsync(TicketTypeRequest request);

        Task DeleteAsync(int id);
    }
}
