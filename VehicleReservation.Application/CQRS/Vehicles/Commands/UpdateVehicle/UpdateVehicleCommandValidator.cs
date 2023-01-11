using FluentValidation;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {

        }
    }
}
