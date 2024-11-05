namespace ReservationServiceBLL.DTO.Requests
{
    public class ReservationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int ShowtimeId { get; set; }
        public int TicketTypeId { get; set; }
        public int Amount { get; set; }
    }
}
