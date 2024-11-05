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
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWorkEF unitOfWork;
        private readonly IMapper mapper;
        private readonly IReservationRepository reservationRepository;
        private readonly ITicketTypeRepository ticketTypeRepository;

        public async Task<IEnumerable<ReservationResponse>> GetAsync()
        {
            var reservations = await reservationRepository.GetAsync();
            return reservations?.Select(mapper.Map<Reservation, ReservationResponse>);
        }

        public async Task<PagedList<ReservationResponse>> GetAsync(ReservationParameters parameters)
        {
            var reservations = await reservationRepository.GetAsync(parameters);
            return reservations?.Map(mapper.Map<Reservation, ReservationResponse>);
        }

        public async Task<IEnumerable<ReservationResponse>> GetTicketsReservationsAsync(int ticketTypeId)
        {
            var ticket = await ticketTypeRepository.GetCompleteEntityAsync(ticketTypeId);
            var reservations = ticket?.Reservations;
            return reservations?.Select(mapper.Map<Reservation, ReservationResponse>);
        }

        public async Task<ReservationResponse> GetByIdAsync(int id)
        {
            var reservation = await reservationRepository.GetCompleteEntityAsync(id);
            return mapper.Map<Reservation, ReservationResponse>(reservation);
        }

        public async Task InsertAsync(ReservationRequest request)
        {
            var reservation = mapper.Map<ReservationRequest, Reservation>(request);
            await reservationRepository.InsertAsync(reservation);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReservationRequest request)
        {
            var reservation = mapper.Map<ReservationRequest, Reservation>(request);
            await reservationRepository.UpdateAsync(reservation);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await reservationRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public ReservationService(IUnitOfWorkEF unitOfWork, IMapper mapper, IReservationRepository reservationRepository, ITicketTypeRepository ticketTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
            this.ticketTypeRepository = ticketTypeRepository;
        }
    }
}
