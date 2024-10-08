using API.Extensions;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateTime DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string? Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Number { get; set; }
    public string? Interests { get; set; }
    public string? Company { get; set; }
    public string? City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = [];
}
