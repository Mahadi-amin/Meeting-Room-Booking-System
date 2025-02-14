﻿namespace Domain.Entities
{
    public class MeetingRoom : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string? Facilities { get; set; }
        public string? Instructions { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public string? Image { get; set; }
        public string? QRCode { get; set; }
        public string? Color { get; set; }
        public bool? Status { get; set; } = true;

        // Navigation property
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
