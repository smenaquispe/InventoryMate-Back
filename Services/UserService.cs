namespace InventoryMate.Services;
using InventoryMate.Models;
using InventoryMate.Repositories;

public interface IUserService {
    Task<User?> GetUserById(string id);
    Task<List<User>> GetUsers();
    Task<User?> CreateUser(User user);
    Task<User?> GetUserByEmailAndPassword(string id, string email, string password);
    Task<User?> UpdateUser(User user);
    Task<User?> DeleteUser(string id);
}

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserById(string id) {
        return await _userRepository.GetById(id);
    }

    public async Task<List<User>> GetUsers() {
        return await _userRepository.Get();
    }

    public async Task<User?> CreateUser(User user) {
        user.Id = Guid.NewGuid().ToString();
        return await _userRepository.Create(user);
    }

    public async Task<User?> GetUserByEmailAndPassword(string id, string email, string password) {
        return await _userRepository.GetByEmailAndPassword(id, email, password);
    }

    public async Task<User?> UpdateUser(User user) {
        return await _userRepository.Update(user);
    }

    public async Task<User?> DeleteUser(string id) {
        return await _userRepository.Delete(id);
    }
}