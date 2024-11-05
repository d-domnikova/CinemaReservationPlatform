using FluentValidation;
using ReservationServiceBLL.DTO.Requests;

namespace ReservationServiceBLL.Validations.Requests
{
    public class ReservationRequestValidation : AbstractValidator<ReservationRequest>
    {
        public ReservationRequestValidation()
        {
            RuleFor(reservation => reservation.Id)
                .GreaterThan(0)
                .WithMessage(reservation => $"{nameof(reservation.Id)} should be greater than 0.")
                .NotEmpty()
                .WithMessage(reservation => $"{nameof(reservation.Id)} can't be empty.");

            RuleFor(reservation => reservation.Name)
                .NotEmpty()
                .WithMessage(reservation => $"{nameof(reservation.Name)} can't be empty.")  
                .Length(70)
                .WithMessage(reservation => $"{nameof(reservation.Name)} should be less than 70 characters.");

            RuleFor(reservation => reservation.ShowtimeId)
                .GreaterThan(0)
                .WithMessage(reservation => $"{nameof(reservation.ShowtimeId)} should be greater than 0.")
                .NotEmpty()
                .WithMessage(reservation => $"{nameof(reservation.ShowtimeId)} can't be empty.");
        }
    }
}
