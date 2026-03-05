namespace NotificationSystem.Models;

public class UserNotification
{
    public int Id { get; set; }
    public bool IsRead { get; set; }

    public int RecipientId { get; set; }
    public int NotificationId { get; set; }

    public required User Recipient { get; set; }
    public required Notification Notification { get; set; }
}