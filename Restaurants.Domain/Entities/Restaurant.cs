﻿namespace Restaurants.Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? Contactnumber { get; set; }

        public Address? Address { get; set; }
        public List<Dish> Dishes { get; set; } = new();

        public string OwnerId { get; set; } = default!;
        public User Owner { get; set; } = default!;
    }
}