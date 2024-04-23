namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface ISaleService {
    Task<Sale?> GetSaleById(string id);
    Task<List<Sale>> GetSales();
    Task<Sale?> CreateSale(Sale sale);
    Task<Sale?> UpdateSale(Sale sale);
    Task<Sale?> DeleteSale(string id);
}

public class SaleService : ISaleService {
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public async Task<Sale?> GetSaleById(string id) {
        return await _saleRepository.GetSaleById(id);
    }

    public async Task<List<Sale>> GetSales() {
        return await _saleRepository.GetSales();
    }

    public async Task<Sale?> CreateSale(Sale sale) {
        sale.Id = Guid.NewGuid().ToString();
        return await _saleRepository.CreateSale(sale);
    }

    public async Task<Sale?> UpdateSale(Sale sale) {
        return await _saleRepository.UpdateSale(sale);
    }

    public async Task<Sale?> DeleteSale(string id) {
        return await _saleRepository.DeleteSale(id);
    }
}