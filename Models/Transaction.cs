namespace InventoryMate.Models;

public class Transaction {
    public string? Id { set; get; }
    public string? Type { set; get; }
    public string? ProductId { set; get; }
    public int Quantity { set; get; }

    public DateTime Date { set; get; }
    public string? UserId { set; get; }
    public string? StoreId { set; get; }
}