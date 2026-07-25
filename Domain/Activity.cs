using Domain.Common;

namespace Domain;

public class Activity : BaseEntity<string>
{
    public required string Title  { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required DateTime Date { get; set; }
    public bool IsCancelled { get; set; }
    public required int MaxParticipants { get; set; }

    // Location
    public required string City { get; set; }
    public required string Venue { get; set; }
     public double Latitude { get; set; }
    public double Longitude { get; set; }
  

     // navigation properties
    // public ICollection<ActivityAttendee> Attendees { get; set; } = [];
    // public ICollection<Comment> Comments { get; set; } = [];

}
