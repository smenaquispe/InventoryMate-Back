namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface ITransactionService {
    Task<Transaction?> GetTransactionById(string id);
    Task<List<Transaction>> GetTransactions();
    Task<Transaction?> CreateTransaction(Transaction transaction);
    Task<Transaction?> UpdateTransaction(Transaction transaction);
    Task<Transaction?> DeleteTransaction(string id);
}

public class TransactionService : ITransactionService {
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository) {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction?> GetTransactionById(string id) {
        return await _transactionRepository.GetTransactionById(id);
    }

    public async Task<List<Transaction>> GetTransactions() {
        return await _transactionRepository.GetTransactions();
    }

    public async Task<Transaction?> CreateTransaction(Transaction transaction) {
        transaction.Id = Guid.NewGuid().ToString();
        return await _transactionRepository.CreateTransaction(transaction);
    }

    public async Task<Transaction?> UpdateTransaction(Transaction transaction) {
        return await _transactionRepository.UpdateTransaction(transaction);
    }

    public async Task<Transaction?> DeleteTransaction(string id) {
        return await _transactionRepository.DeleteTransaction(id);
    }
}