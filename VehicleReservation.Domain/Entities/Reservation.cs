using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime EndTo { get; set; }
    }
}
