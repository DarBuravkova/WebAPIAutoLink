﻿namespace WebAPIAutoLink.DTO
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool IsRented { get; set; } = false;
    }
}
