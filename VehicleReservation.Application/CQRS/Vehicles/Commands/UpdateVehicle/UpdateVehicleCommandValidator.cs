using FluentValidation;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            //TODO:
            //I didn't have a time, but here we would probably like to check if the whole Dto model is present and filled with data, or just some props because its update
        }
    }
}
