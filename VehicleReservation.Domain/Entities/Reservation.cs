namespace VehicleReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
       
        public DateTime StartFrom { get; set; }
        public DateTime EndTo { get; set; }
        public bool IsDeleted { get; set; }
        
        public Vehicle Vehicle { get; set; }
        public string VehicleId { get; set; }
    }
}
