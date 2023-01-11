using FluentValidation;

namespace VehicleReservation.Application.CQRS.Reservations.Commands.AddReservationForVehicle
{
    public class AddReservationForVehicleCommandValidator : AbstractValidator<AddReservationForVehicleCommand>
    {
        public AddReservationForVehicleCommandValidator()
        {

        }
    }
}
