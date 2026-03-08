using Intake.Models;

namespace Intake.Repository;

public interface IUserRepository
{
    public Task AddUserAsync(User user);
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<List<User?>> SearchUsersAsync(string searchText);
    public Task<List<User?>> GetUsersAsync();
    public Task UpdateUserAsync(User user);
    public Task<bool> DeleteUserAsync(int userId);
}