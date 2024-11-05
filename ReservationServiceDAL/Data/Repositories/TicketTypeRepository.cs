using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Exceptions;
using ReservationServiceDAL.Interfaces.Repositories;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceDAL.Data.Repositories
{
    public class TicketTypeRepository : GenericRepositoryEF<TicketType>, ITicketTypeRepository
    {
        public TicketTypeRepository(ReservationServiceContext dbContext) : base(dbContext)
        {
        }

        public async override Task<TicketType> GetCompleteEntityAsync(int id)
        {
            var ticket = await table
                         .SingleOrDefaultAsync(ticket => ticket.Id == id);

            return ticket ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }

        public async Task<PagedList<TicketType>> GetAsync(TicketTypeParameters parameters)
        {

            return await PagedList<TicketType>.ToPagedListAsync(
               table,
               parameters.PageNumber,
               parameters.PageSize);
        }
    }
}
