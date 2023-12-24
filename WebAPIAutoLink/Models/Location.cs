namespace WebAPIAutoLink.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
