namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using Microsoft.EntityFrameworkCore;

public interface ISaleRepository {
    Task<Sale?> GetSaleById(string id);
    Task<List<Sale>> GetSales();
    Task<Sale?> CreateSale(Sale sale);
    Task<Sale?> UpdateSale(Sale sale);
    Task<Sale?> DeleteSale(string id);
}

public class SaleRepository : ISaleRepository {
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Sale?> GetSaleById(string id) {
        return await _context.Sales.FindAsync(id);
    }

    public async Task<List<Sale>> GetSales() {
        return await _context.Sales.ToListAsync();
    }

    public async Task<Sale?> CreateSale(Sale sale) {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale?> UpdateSale(Sale sale) {
        var findSale = await _context.Sales.FindAsync(sale.Id);
        if(findSale == null) return null;
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale?> DeleteSale(string id) {
        var sale = await _context.Sales.FindAsync(id);
        if(sale == null) return null;
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}