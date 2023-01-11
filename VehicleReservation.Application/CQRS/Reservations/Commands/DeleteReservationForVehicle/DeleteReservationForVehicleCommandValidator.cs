using FluentValidation;

namespace VehicleReservation.Application.CQRS.Reservations.Commands.DeleteReservationVehicle
{
    public class DeleteReservationForVehicleCommandValidator : AbstractValidator<DeleteReservationForVehicleCommand>
    {
        public DeleteReservationForVehicleCommandValidator()
        {

        }
    }
}
