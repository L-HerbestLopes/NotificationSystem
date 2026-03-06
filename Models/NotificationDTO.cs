namespace NotificationSystem.Models;

public class NotificationDTO
{
    public int Id { get; set; }
    public required string Message { get; set; }

    public int SenderId { get; set; }

    public required List<int> Recipients { get; set; }
}