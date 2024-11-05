using FluentValidation;
using ReservationServiceBLL.DTO.Requests;

namespace ReservationServiceBLL.Validations.Requests
{
    public class TicketTypeRequestValidation : AbstractValidator<TicketTypeRequest>
    {
        public TicketTypeRequestValidation()
        {
            RuleFor(ticket => ticket.Id)
            .NotEmpty()
               .WithMessage(ticket => $"{nameof(ticket.Id)} can't be empty.");

            RuleFor(ticket => ticket.Type)
                .NotEmpty()
                .WithMessage(ticket => $"{nameof(ticket.Type)} can't be empty.")
                .Length(50)
                .WithMessage(ticket => $"{nameof(ticket.Type)} should be less than 50 characters.");
        }
    }
}
