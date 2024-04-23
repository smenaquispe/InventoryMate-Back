namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface IStoreService {
    Task<Store> CreateStore(Store store);
    Task<Store?> GetStore(string storeId);
    Task<List<Store>> GetStores(string ownerId);
    Task<Store?> UpdateStore(Store store);
    Task<bool> DeleteStore(string storeId);
}

public class StoreService : IStoreService {
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository) {
        _storeRepository = storeRepository;
    }

    public async Task<Store> CreateStore(Store store) {
        store.Id = Guid.NewGuid().ToString();
        return await _storeRepository.CreateStore(store);
    }

    public async Task<Store?> GetStore(string storeId) {
        return await _storeRepository.GetStore(storeId);
    }

    public async Task<List<Store>> GetStores(string ownerId) {
        return await _storeRepository.GetStores(ownerId);
    }

    public async Task<Store?> UpdateStore(Store store) {
        return await _storeRepository.UpdateStore(store);
    }

    public async Task<bool> DeleteStore(string storeId) {
        return await _storeRepository.DeleteStore(storeId);
    }
}