namespace InventoryMate.Models;

public class Notification {
    public string? Id { set; get; }
    public string? UserId { set; get; }
    public string? Type { set; get; }
    public string? Message { set; get; }
    public DateTime Date { set; get; }
}