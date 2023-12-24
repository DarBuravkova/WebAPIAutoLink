﻿using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte Photo { get; set; }
        public string Role { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; }

        public int AuthId { get; set; }
        public Authorization Authorizations { get; set; }
    }
}
