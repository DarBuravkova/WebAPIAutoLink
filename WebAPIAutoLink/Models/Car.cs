namespace WebAPIAutoLink.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public byte Photo { get; set; }
        public bool IsRented { get; set; }
        public FleetOwner FleetOwners { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Location Locations { get; set; }
        public int CarStatusId { get; set; }
        public CarStatus CarStatus { get; set; }
    }
}
