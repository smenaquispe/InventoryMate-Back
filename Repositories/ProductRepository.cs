namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using Microsoft.EntityFrameworkCore;


public interface IProductRepository {
    Task<Product?> GetProductById(string id);
    Task<List<Product>> GetProducts();
    Task<Product?> CreateProduct(Product product);

    Task<IEnumerable<Product>> CreateAndUpdateProducts(IEnumerable<Product> products);
    Task<Product?> UpdateProduct(Product product);
    Task<Product?> DeleteProduct(string id);
}

public class ProductRepository : IProductRepository {
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Product?> GetProductById(string id) {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetProducts() {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> CreateProduct(Product product) {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async  Task<IEnumerable<Product>> CreateAndUpdateProducts(IEnumerable<Product> products)
    {
        var productsIHave = await _context.Products.ToListAsync();
        foreach (var product in products)
        {
            var productFind = await _context.Products.FindAsync(product.Id);
            if(productFind == null) {
                await _context.Products.AddAsync(product);
            } else {
                _context.Products.Update(product);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Product?> UpdateProduct(Product product) {
        var findProduct = await _context.Products.FindAsync(product.Id);
        if(findProduct == null) return null;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeleteProduct(string id) {
        var product = await _context.Products.FindAsync(id);
        if(product == null) return null;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
}