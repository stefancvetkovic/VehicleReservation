using FluentValidation;
using System.Security.Cryptography.X509Certificates;
using VehicleReservation.Application.Intefaces.Entities;

namespace VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        private readonly IVehicleRepositoryAsync _repositoryVehicle;
        public AddVehicleCommandValidator(IVehicleRepositoryAsync repositoryVehicle)
        {
            _repositoryVehicle = repositoryVehicle;

            RuleFor(x => x.Vehicle.Model)
                .NotNull()
                .WithMessage("Model is required.");
            RuleFor(x => x.Vehicle.Maker)
                .NotNull()
                .WithMessage("Maker is required.");
            RuleFor(x => x.Vehicle.UniqueId)
                .NotNull()
                .NotEmpty()
                .WithMessage("UniqueId is required.")
                .Must(CheckKeyComposition).WithMessage("UniqueId needs to be in fomrat C<number>");

            RuleFor(x => x.Vehicle.UniqueId)
                .Must(x => AlreadyInUse(x))
                .WithMessage("This id is already in use.");
        }

        private bool AlreadyInUse(string key)
        {
            return _repositoryVehicle.HasFreeVehicleId(key);
        }

        private bool CheckKeyComposition(string key)
        {
            var firstChar = key.Substring(0,1);
            var secondChar = key.Substring(1, key.Length-1);

            bool res = int.TryParse(secondChar, out int number);

            return firstChar.Equals("C") && res;
        }
    }
}