using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Exceptions;
using ReservationServiceDAL.Interfaces.Repositories;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceDAL.Data.Repositories
{
    public class ReservationRepository : GenericRepositoryEF<Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationServiceContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Reservation> GetCompleteEntityAsync(int id)
        {
            var reservation = await table.Include(reservation => reservation.Ticket)
                         .SingleOrDefaultAsync(reservation => reservation.Id == id);

            return reservation ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }

        public async Task<PagedList<Reservation>> GetAsync(ReservationParameters parameters)
        {

            IQueryable<Reservation> source = table.Include(reservation => reservation.Ticket);

            SearchByName(ref source, parameters.Name);
            SearchByShowtimeId(ref source, parameters.ShowtimeId);
            SearchByTicketTypeId(ref source, parameters.TicketTypeId);

            return await PagedList<Reservation>.ToPagedListAsync(
                source,
                parameters.PageNumber,
                parameters.PageSize);
        }

        private static void SearchByName(ref IQueryable<Reservation> source, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            source = source.Where(project => project.Name.Contains(name));
        }

        private static void SearchByShowtimeId(ref IQueryable<Reservation> source, int? showtimeId)
        {
            if (showtimeId is null || showtimeId == 0)
            {
                return;
            }

            source = source.Where(reservation => reservation.ShowtimeId == showtimeId);
        }

        private static void SearchByTicketTypeId(ref IQueryable<Reservation> source, int? ticketTypeId)
        {
            if (ticketTypeId is null || ticketTypeId == 0)
            {
                return;
            }

            source = source.Where(reservation => reservation.TicketTypeId == ticketTypeId);
        }

    }
}
