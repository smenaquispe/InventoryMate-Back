namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using Microsoft.EntityFrameworkCore;

public interface INotificationRepository {
    Task<Notification?> GetNotificationById(string id);
    Task<List<Notification>> GetNotifications();
    Task<Notification?> CreateNotification(Notification notification);
    Task<Notification?> UpdateNotification(Notification notification);
    Task<Notification?> DeleteNotification(string id);
}

public class NotificationRepository : INotificationRepository {
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Notification?> GetNotificationById(string id) {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task<List<Notification>> GetNotifications() {
        return await _context.Notifications.ToListAsync();
    }

    public async Task<Notification?> CreateNotification(Notification notification) {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<Notification?> UpdateNotification(Notification notification) {
        var findNotification = await _context.Notifications.FindAsync(notification.Id);
        if(findNotification == null) return null;
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<Notification?> DeleteNotification(string id) {
        var notification = await _context.Notifications.FindAsync(id);
        if(notification == null) return null;
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

}