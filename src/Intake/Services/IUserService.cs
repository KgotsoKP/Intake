using Intake.Common;
using Intake.Models;

namespace Intake.Services;

public interface IUserService
{
    public Task<List<User?>> GetUsers();

    public Task<Result<User?>> GetUserById(int userId);

    public Task<List<User?>> SearchUsers(string query);

    public Task<Result<bool>> AddUser(User? user);

    public Task UpdateUser(User? user);

    public Task<Result<bool>> DeleteUser(int userId);
}