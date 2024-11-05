namespace ReservationServiceBLL.DTO.Responses
{
    public class ReservationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int ShowtimeId { get; set; }

        public int TicketTypeId { get; set; }
        public TicketTypeResponse Ticket { get; set; }

        public int Amount { get; set; }
        public decimal TotalCost { get; set; }
        public bool isCancelable { get; set; }
        public DateTime TimeOfReservation { get; set; }
    }
}
