using Domain.Entities;

public class Booking : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid MeetingRoomId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Purpose { get; set; }
    public string RepeatOption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public MeetingRoom MeetingRoom { get; set; }

}
