namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;
    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<Store>> GetStore(string id)
    {
        var store = await _storeService.GetStore(id!);
        if (store == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetStore), store);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores(string ownerId)
    {
        var stores = await _storeService.GetStores(ownerId);
        return CreatedAtAction(nameof(GetStores), stores.ToList());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Store>> CreateStore(Store store)
    {
        var createdStore = await _storeService.CreateStore(store);
        return CreatedAtAction(nameof(GetStore), new { id = createdStore?.Id }, createdStore);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Store>> UpdateStore(string id, Store store)
    {
        var findStore = await _storeService.GetStore(id);
        if(findStore == null)
        {
            return NotFound();
        }
        var updatedStore = await _storeService.UpdateStore(store);
        return CreatedAtAction(nameof(GetStore), new { id = updatedStore?.Id }, updatedStore);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteStore(string id)
    {
        var findStore = await _storeService.GetStore(id);
        if(findStore == null)
        {
            return NotFound();
        }
        var isDeleted = await _storeService.DeleteStore(id!);
        return CreatedAtAction(nameof(DeleteStore), isDeleted);
    }
}