namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using InventoryMate.Dto;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;
    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<Sale>> GetSale(string id)
    {
        var sale = await _saleService.GetSaleById(id!);
        if (sale == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetSale), sale);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
    {
        var sales = await _saleService.GetSales();
        return CreatedAtAction(nameof(GetSales), sales.ToList());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Sale>> CreateSale(Sale sale)
    {
        var createdSale = await _saleService.CreateSale(sale);
        return CreatedAtAction(nameof(GetSale), new { id = createdSale?.Id }, createdSale);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Sale>> UpdateSale(string id, Sale sale)
    {
        var findSale = await _saleService.GetSaleById(id);
        if(findSale == null)
        {
            return NotFound();
        }
        var updatedSale = await _saleService.UpdateSale(sale);
        return CreatedAtAction(nameof(GetSale), new { id = updatedSale?.Id }, updatedSale);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteSale(string id)
    {
        var findSale = await _saleService.GetSaleById(id);
        if(findSale == null)
        {
            return NotFound();
        }
        var isDeleted = await _saleService.DeleteSale(id!);
        return CreatedAtAction(nameof(DeleteSale), isDeleted);
    }
}