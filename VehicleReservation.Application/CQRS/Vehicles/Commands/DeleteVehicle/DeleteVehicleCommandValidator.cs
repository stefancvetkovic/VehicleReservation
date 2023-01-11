using FluentValidation;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {

        }
    }
}
