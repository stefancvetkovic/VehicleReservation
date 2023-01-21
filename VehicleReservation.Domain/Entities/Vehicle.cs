using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VehicleReservation.Domain.Entities
{
    public class Vehicle
    {
        public string? UniqueId { get; set; }
        public string? Model { get; set; }
        public string? Maker { get; set; }
        public ICollection<Reservation>? Reservation { get; set; }
    }
}
