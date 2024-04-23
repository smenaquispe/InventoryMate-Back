namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using Microsoft.EntityFrameworkCore;


public interface IStoreRepository {
    Task<Store> CreateStore(Store store);
    Task<Store?> GetStore(string storeId);
    Task<List<Store>> GetStores(string ownerId);
    Task<Store?> UpdateStore(Store store);
    Task<bool> DeleteStore(string storeId);
}

public class StoreRepository : IStoreRepository {
    private readonly AppDbContext _context;

    public StoreRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Store> CreateStore(Store store) {
        _context.Stores.Add(store);
        await _context.SaveChangesAsync();
        return store;
    }

    public async Task<Store?> GetStore(string storeId) {
        return await _context.Stores.FindAsync(storeId);
    }

    public async Task<List<Store>> GetStores(string ownerId) {
        return await _context.Stores.Where(s => s.OwnerId == ownerId).ToListAsync();
    }

    public async Task<Store?> UpdateStore(Store store) {
        var findStore = await _context.Stores.FindAsync(store.Id);
        if(findStore == null) return null;
        _context.Stores.Update(store);
        await _context.SaveChangesAsync();
        return store;
    }

    public async Task<bool> DeleteStore(string storeId) {
        var store = await _context.Stores.FindAsync(storeId);
        if(store == null) return false;
        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
        return true;
    }

}