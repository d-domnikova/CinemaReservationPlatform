using ReservationServiceBLL.DTO.Requests;
using ReservationServiceBLL.DTO.Responses;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceBLL.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationResponse>> GetAsync();

        Task<PagedList<ReservationResponse>> GetAsync(ReservationParameters parameters);

        Task<IEnumerable<ReservationResponse>> GetTicketsReservationsAsync(int ticketTypeId);

        Task<ReservationResponse> GetByIdAsync(int id);

        Task InsertAsync(ReservationRequest request);

        Task UpdateAsync(ReservationRequest request);

        Task DeleteAsync(int id);
    }
}
