using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;
using NotificationSystem.Models;

namespace NotificationSystem.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController(UserContext userContext) : ControllerBase
{
    private readonly UserContext _userContext = userContext;

    [HttpPost]
    public async Task<ActionResult<User>> CreateNotification(NotificationDTO dto)
    {
        if(string.IsNullOrWhiteSpace(dto.Message)
           || dto.Recipients.Count == 0)
        {
            return BadRequest();
        }

        var sender = await _userContext.Users.FindAsync(dto.SenderId);
        if(sender is null) return BadRequest();

        var notification = new Notification
        {
            Message = dto.Message,
            SenderId = dto.SenderId,
            Sender = sender
        };

        _userContext.Notifications.Add(notification);
        await _userContext.SaveChangesAsync();

        var recipients = await _userContext.Users
            .Where(u => dto.Recipients.Contains(u.Id))
            .ToListAsync();

        var userNotifications = recipients.Select(recipient => new UserNotification
        {
            RecipientId = recipient.Id,
            NotificationId = notification.Id,
            Recipient = recipient,
            Notification = notification
        }).ToList();

        _userContext.UserNotifications.AddRange(userNotifications);
        await _userContext.SaveChangesAsync();

        return Ok(notification);
    }
}