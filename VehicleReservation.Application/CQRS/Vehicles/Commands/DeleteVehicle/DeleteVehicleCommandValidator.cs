using FluentValidation;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            //TODO:
            //I didn't have a time, but here we would probably like to check if id (for exaample: "C1") is there, and id is not null or empty.
        }
    }
}
