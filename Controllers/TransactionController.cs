namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<Transaction>> GetTransaction(string id)
    {
        var transaction = await _transactionService.GetTransactionById(id!);
        if (transaction == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetTransaction), transaction);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        var transactions = await _transactionService.GetTransactions();
        return CreatedAtAction(nameof(GetTransactions), transactions.ToList());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
    {
        var createdTransaction = await _transactionService.CreateTransaction(transaction);
        return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction?.Id }, createdTransaction);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Transaction>> UpdateTransaction(string id, Transaction transaction)
    {
        var findTransaction = await _transactionService.GetTransactionById(id);
        if(findTransaction == null)
        {
            return NotFound();
        }
        var updatedTransaction = await _transactionService.UpdateTransaction(transaction);
        return CreatedAtAction(nameof(GetTransaction), new { id = updatedTransaction?.Id }, updatedTransaction);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteTransaction(string id)
    {
        var findTransaction = await _transactionService.GetTransactionById(id);
        if(findTransaction == null)
        {
            return NotFound();
        }
        var isDeleted = await _transactionService.DeleteTransaction(id!);
        return CreatedAtAction(nameof(DeleteTransaction), isDeleted);
    }
}