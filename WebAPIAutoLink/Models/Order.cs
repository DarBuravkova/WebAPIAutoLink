namespace WebAPIAutoLink.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime BookingDate { get; set; }
        public int UsageTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsPaid { get; set; }
        public int StartLocationId { get; set; }
        public int EndLocationId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
    }
}
