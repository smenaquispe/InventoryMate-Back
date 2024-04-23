namespace InventoryMate.Models;

public class Product {
    public string? Id { set; get; }
    public string? Name { set; get; }
    public string? Description { set; get; }
    public double Price { set; get; }
    public int Stock { set; get; }
    
    public string? Provider { set; get;}

    public string? StoreId { set; get; }
    public DateTime CreatedAt { set; get; }
    public DateTime UpdatedAt { set; get; }
}