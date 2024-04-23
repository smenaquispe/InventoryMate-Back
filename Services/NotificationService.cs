namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface INotificationService {
    Task<Notification?> GetNotificationById(string id);
    Task<List<Notification>> GetNotifications();
    Task<Notification?> CreateNotification(Notification notification);
    Task<Notification?> UpdateNotification(Notification notification);
    Task<Notification?> DeleteNotification(string id);
}

public class NotificationService : INotificationService {
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository) {
        _notificationRepository = notificationRepository;
    }

    public async Task<Notification?> GetNotificationById(string id) {
        return await _notificationRepository.GetNotificationById(id);
    }

    public async Task<List<Notification>> GetNotifications() {
        return await _notificationRepository.GetNotifications();
    }

    public async Task<Notification?> CreateNotification(Notification notification) {
        notification.Id = Guid.NewGuid().ToString();
        return await _notificationRepository.CreateNotification(notification);
    }

    public async Task<Notification?> UpdateNotification(Notification notification) {
        return await _notificationRepository.UpdateNotification(notification);
    }

    public async Task<Notification?> DeleteNotification(string id) {
        return await _notificationRepository.DeleteNotification(id);
    }
}