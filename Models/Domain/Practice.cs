namespace NamPractice.API.Models.Domain;

public class Practice
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInHours { get; set; }
    public string? PracticeImageUrl { get; set; }
    public Guid PracticeId { get; set; }

    public Guid RegionId { get; set; }

    // Navigation properties
    public Difficulty Difficulty { get; set; }
    public Region Region { get; set; }
}