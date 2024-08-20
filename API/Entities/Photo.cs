namespace API.Entities;

public class Photo
{
  public int Id { get; set; }
  public required string Url { get; set; }
  public bool IsMain { get; set; }
  public string? PlubicId { get; set; }
  public int AppUserId { get; set; }
  public AppUser AppUser { get; set; } = null!;
}