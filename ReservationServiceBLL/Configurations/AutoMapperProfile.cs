using AutoMapper;
using ReservationServiceBLL.DTO.Requests;
using ReservationServiceBLL.DTO.Responses;
using ReservationServiceDAL.Entities;

namespace ReservationServiceBLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateReservationMaps()
        {
            CreateMap<ReservationRequest, Reservation>();
            CreateMap<Reservation, ReservationRequest>();
            CreateMap<Reservation, ReservationResponse>()
               .ForMember(
                response => response.TotalCost,
                options => options.MapFrom(reservation => reservation.Ticket.Price * reservation.Amount)
                );
        }

        private void CreateTicketsMaps()
        {
            CreateMap<TicketTypeRequest, TicketType>();
            CreateMap<TicketType, TicketTypeResponse>();
        }

        public AutoMapperProfile() {
            CreateReservationMaps();
            CreateTicketsMaps();
        }
    }
}
