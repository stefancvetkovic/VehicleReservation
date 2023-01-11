using FluentValidation;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        public AddVehicleCommandValidator()
        {

        }
    }
}
