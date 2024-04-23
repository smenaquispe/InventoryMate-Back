namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using InventoryMate.Dto;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _productService.GetProductById(id!);
        if (product == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetProduct), product);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetProducts();
        return CreatedAtAction(nameof(GetProducts), products.ToList());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        var createdProduct = await _productService.CreateProduct(product);
        return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct?.Id }, createdProduct);
    }

    [HttpPost("upload")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Product>>> UploadProducts(IEnumerable<Product> products)
    {
        var uploadedProducts = await _productService.CreateAndUpdateProducts(products);
        return CreatedAtAction(nameof(UploadProducts), products);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> UpdateProduct(string id, Product product)
    {
        var findProduct = await _productService.GetProductById(id!);
        if (findProduct == null)
        {
            return NotFound();
        }
        var updatedProduct = await _productService.UpdateProduct(product);
        return CreatedAtAction(nameof(UpdateProduct), new { id = updatedProduct?.Id }, updatedProduct);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteProduct(string id)
    {
        var findProduct = await _productService.GetProductById(id!);
        if (findProduct == null)
        {
            return NotFound();
        }
        var isDeleted = await _productService.DeleteProduct(id!);
        return CreatedAtAction(nameof(DeleteProduct), isDeleted);
    }
}