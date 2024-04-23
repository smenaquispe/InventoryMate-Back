namespace InventoryMate.Models;

public class Sale {
    public string? Id { set; get; }
    public string? ProductId { set; get; }
    public int Quantity { set; get; }
    public double Price { set; get; }
    public DateTime Date { set; get; }
    public string? UserId { set; get; }
    public string? StoreId { set; get; }
}