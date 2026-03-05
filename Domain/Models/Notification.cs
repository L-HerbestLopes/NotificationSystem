namespace NotificationSystem.Domain.Models;

public class Notification
{
    public int Id { get; set; }
    public required string Message { get; set; }

    public int SenderId { get; set; }

    public required User Sender { get; set; }
}