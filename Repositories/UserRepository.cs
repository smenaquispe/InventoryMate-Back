namespace InventoryMate.Repositories;
using InventoryMate.Models;
using InventoryMate.Data;
using InventoryMate.Utilities;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository {
    Task<List<User>> Get();
    Task<User?> GetById(string id);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByEmailAndPassword(string id, string email, string password);
    Task<User?> Create(User user);
    Task<User?> Update(User user);
    Task<User?> Delete(string id);
}

public class UserRepository : IUserRepository {

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<List<User>> Get() {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(string id) {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmail(string email) {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailAndPassword(string id, string email, string password) {
        var userWithSameEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Id == id);

        if (userWithSameEmail == null) return null;

        var isPasswordCorrect = HashCrypter.VerifyPassword(password, userWithSameEmail.Password!);
        return isPasswordCorrect ? userWithSameEmail : null;
    }

    public async Task<User?> Create(User user) {
        user.Password = HashCrypter.HashPassword(user.Password!);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Update(User user) {
        var findUser = await _context.Users.FindAsync(user.Id);
        if (findUser == null) return null;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Delete(string id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}