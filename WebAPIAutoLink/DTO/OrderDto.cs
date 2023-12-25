namespace WebAPIAutoLink.DTO
{
    public class OrderDto
    {

        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int UsageTime { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public bool IsPaid { get; set; } = false;
        public int StartLocationId { get; set; }
        public int EndLocationId { get; set; }
    }
}
