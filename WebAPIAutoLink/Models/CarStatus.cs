namespace WebAPIAutoLink.Models
{
    public class CarStatus
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int OdometerReading { get; set; }
        public int FuelLevel { get; set; }
        public string MaintenanceStatus { get; set; } = string.Empty;

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
