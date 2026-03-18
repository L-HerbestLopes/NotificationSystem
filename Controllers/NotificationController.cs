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

    // registers a notification
    [HttpPost]
    public async Task<ActionResult<Notification>> CreateNotification(NotificationDTO dto)
    {
        if(string.IsNullOrWhiteSpace(dto.Message))
            return BadRequest("message shouldn't be empty");
        
        if(dto.Recipients.Count == 0)
            return BadRequest("notification must have recipients");

        var sender = await _userContext.Users.FindAsync(dto.SenderId);
        if(sender is null) return BadRequest("sender doesn't exist");
        if(sender.Access == UserAccess.Common) return BadRequest("sender isn't allowed to send notifications");

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

    // get notification by id
    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<Notification>> GetNotificationById(int id)
    {
        Notification? notification = await _userContext.Notifications.FindAsync(id);

        if(notification is null) return NotFound();
        return Ok(notification);
    }

    // get all notifications
    [HttpGet]
    public async Task<ActionResult<List<Notification>>> GetAllNotifications()
    {
        return await _userContext.Notifications.ToListAsync();
    }

    // search a notification with a recipient id/email, can filter for only unread messages
    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<List<UserNotification>>> GetByUser([FromQuery]string? email, [FromQuery]int? userid, [FromQuery]bool showread = true)
    {
        if(email is null && userid is null) return BadRequest("An user email or id is needed for search");

        var user = await _userContext.Users.FirstOrDefaultAsync<User>(u => u.Email == email || u.Id == userid);

        if(user is null) return NotFound();

        var notifications = await _userContext.UserNotifications
            .Where(n => n.RecipientId == user.Id && (!n.IsRead || showread))
            .ToListAsync();

        return Ok(notifications);
    }
}