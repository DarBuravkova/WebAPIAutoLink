﻿namespace WebAPIAutoLink.Models
{
    public class FleetOwner
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
