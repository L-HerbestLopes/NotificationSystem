using System.ComponentModel.DataAnnotations;

namespace NotificationSystem.Models;

public class User
{
    public int Id { get; set; }
    [Required] public required string Name { get; set; }
    [Required][EmailAddress] public required string Email { get; set; }
    public UserAccess Access { get; set; }
}