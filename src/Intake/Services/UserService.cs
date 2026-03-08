using System.Xml.Linq;
using Intake.Common;
using Intake.Models;
using Intake.Repository;

namespace Intake.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> AddUser(User user)
    {
        try
        {
            await _userRepository.AddUserAsync(user);
            return true;
        }
        catch (Exception e)
        {
            return UserErrors.FailedToCreateError;
        }
    }

    public async Task<Result<User?>> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        return user;
    }

    public async Task<List<User?>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        return users;
    }

    public async Task<List<User?>> SearchUsers(string query)
    {
        var users = await _userRepository.SearchUsersAsync(query);
        return users;
    }

    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task<Result<bool>> DeleteUser(int userId)
    {
        try
        {
            var result = await _userRepository.DeleteUserAsync(userId);

            if (!result)
            {
                return UserErrors.NotFound(userId);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}