namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<Notification>> GetNotification(string id)
    {
        var notification = await _notificationService.GetNotificationById(id!);
        if (notification == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetNotification), notification);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
    {
        var notifications = await _notificationService.GetNotifications();
        return CreatedAtAction(nameof(GetNotifications), notifications.ToList());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
    {
        var createdNotification = await _notificationService.CreateNotification(notification);
        return CreatedAtAction(nameof(GetNotification), new { id = createdNotification?.Id }, createdNotification);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Notification>> UpdateNotification(string id, Notification notification)
    {
        var findNotification = await _notificationService.GetNotificationById(id);
        if(findNotification == null) 
        {
            return NotFound();
        }
        var updatedNotification = await _notificationService.UpdateNotification(notification);
        return CreatedAtAction(nameof(GetNotification), new { id = updatedNotification?.Id }, updatedNotification);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteNotification(string id)
    {
        var findNotification = await _notificationService.GetNotificationById(id);
        if(findNotification == null) 
        {
            return NotFound();
        }
        var isDeleted = await _notificationService.DeleteNotification(id!);
        return CreatedAtAction(nameof(DeleteNotification), isDeleted);
    }

}