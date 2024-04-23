namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using Microsoft.EntityFrameworkCore;

public interface ITransactionRepository {
    Task<Transaction?> GetTransactionById(string id);
    Task<List<Transaction>> GetTransactions();
    Task<Transaction?> CreateTransaction(Transaction transaction);
    Task<Transaction?> UpdateTransaction(Transaction transaction);
    Task<Transaction?> DeleteTransaction(string id);
}

public class TransactionRepository : ITransactionRepository {

    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Transaction?> GetTransactionById(string id) {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<List<Transaction>> GetTransactions() {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction?> CreateTransaction(Transaction transaction) {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> UpdateTransaction(Transaction transaction) {
        var findTransaction = await _context.Transactions.FindAsync(transaction.Id);
        if(findTransaction == null) return null;
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> DeleteTransaction(string id) {
        var transaction = await _context.Transactions.FindAsync(id);
        if(transaction == null) return null;
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}