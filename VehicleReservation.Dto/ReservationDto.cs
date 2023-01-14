namespace VehicleReservation.Dto
{
    public class ReservationDto
    {
        public DateTime StartFrom { get; set; }
        public DateTime EndTo { get; set; }
        public bool IsDeleted { get; set; }
        public string? VehicleId { get; set; }
    }
}
