using AutoMapper;
using ReservationServiceBLL.DTO.Requests;
using ReservationServiceBLL.DTO.Responses;
using ReservationServiceBLL.Interfaces;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Interfaces;
using ReservationServiceDAL.Interfaces.Repositories;
using ReservationServiceDAL.Pagination;
using ReservationServiceDAL.Parameters;

namespace ReservationServiceBLL.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly IUnitOfWorkEF unitOfWork;
        private readonly IMapper mapper;
        private readonly ITicketTypeRepository ticketTypeRepository;

        public async Task<IEnumerable<TicketTypeResponse>> GetAsync()
        {
            var tickets = await ticketTypeRepository.GetAsync();
            return tickets?.Select(mapper.Map<TicketType, TicketTypeResponse>);
        }

        public async Task<PagedList<TicketTypeResponse>> GetAsync(TicketTypeParameters parameters)
        {
            var tickets = await ticketTypeRepository.GetAsync(parameters);
            return tickets?.Map(mapper.Map<TicketType, TicketTypeResponse>);
        }

        public async Task<TicketTypeResponse> GetByIdAsync(int id)
        {
            var ticket = await ticketTypeRepository.GetCompleteEntityAsync(id);
            return mapper.Map<TicketType, TicketTypeResponse>(ticket);
        }

        public async Task InsertAsync(TicketTypeRequest request)
        {
            var ticket = mapper.Map<TicketTypeRequest, TicketType>(request);
            await ticketTypeRepository.InsertAsync(ticket);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketTypeRequest request)
        {
            var ticket = mapper.Map<TicketTypeRequest, TicketType>(request);
            await ticketTypeRepository.UpdateAsync(ticket);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await ticketTypeRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public TicketTypeService(IUnitOfWorkEF unitOfWork, IMapper mapper, ITicketTypeRepository ticketTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.ticketTypeRepository = ticketTypeRepository;
        }
    }
}
