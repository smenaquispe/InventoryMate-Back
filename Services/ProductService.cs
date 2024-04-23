namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface IProductService {
    Task<Product?> GetProductById(string id);
    Task<List<Product>> GetProducts();
    Task<Product?> CreateProduct(Product product);
    Task<IEnumerable<Product>> CreateAndUpdateProducts(IEnumerable<Product> products);
    Task<Product?> UpdateProduct(Product product);
    Task<Product?> DeleteProduct(string id);
}

public class ProductService : IProductService {
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task<Product?> GetProductById(string id) {
        return await _productRepository.GetProductById(id);
    }

    public async Task<List<Product>> GetProducts() {
        return await _productRepository.GetProducts();
    }

    public async Task<Product?> CreateProduct(Product product) {
        product.Id = Guid.NewGuid().ToString();
        return await _productRepository.CreateProduct(product);
    }

    public async Task<IEnumerable<Product>> CreateAndUpdateProducts(IEnumerable<Product> products){
        var listProducts = _productRepository.CreateAndUpdateProducts(products);
    }

    public async Task<Product?> UpdateProduct(Product product) {
        return await _productRepository.UpdateProduct(product);
    }

    public async Task<Product?> DeleteProduct(string id) {
        return await _productRepository.DeleteProduct(id);
    }
}