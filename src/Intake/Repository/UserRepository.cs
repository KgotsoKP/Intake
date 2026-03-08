using System.Xml.Linq;
using Intake.Models;

namespace Intake.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;

    public UserRepository(string filePath)
    {
        _filePath = filePath;
    }

    private async Task<XDocument> LoadUserDataAsync()
    {
        var xml = await File.ReadAllTextAsync(_filePath);
        return XDocument.Parse(xml);
    }

    private async Task SaveUserDataAsync(XDocument userData)
    {
        await File.WriteAllTextAsync(_filePath, userData.ToString());
    }

    public async Task AddUserAsync(User user)
    {
        var userData = await LoadUserDataAsync();

        Random random = new Random();
        int id = random.Next(500);

        userData.Root.Add(
            new XElement("User",
                new XElement("Id", id),
                new XElement("Name", user.Name),
                new XElement("Surname", user.Surname),
                new XElement("CellphoneNumber", user.CellphoneNumber)
            )
        );

        await SaveUserDataAsync(userData);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var userDataXDocument = await LoadUserDataAsync();

        var el = userDataXDocument.Root.Elements("User")
            .FirstOrDefault(e => (int)e.Element("Id") == userId);

        if (el == null) return null;

        var user = new User
        {
            Id = (int)el.Element("Id"),
            Name = (string)el.Element("Name"),
            Surname = (string)el.Element("Surname"),
            CellphoneNumber = (string)el.Element("CellphoneNumber")
        };

        return user;
    }

    public async Task<List<User?>> SearchUsersAsync(string searchText)
    {
        var userDataXDocument = await LoadUserDataAsync();

        var users = userDataXDocument.Root.Elements("User")
            .Select(el => new User()
            {
                Id = (int)el.Element("Id"),
                Name = (string)el.Element("Name"),
                Surname = (string)el.Element("Surname"),
                CellphoneNumber = (string)el.Element("CellphoneNumber"),
            })
            .Where(u =>
                u.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                u.Surname.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                u.CellphoneNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return users;
    }

    public async Task<List<User?>> GetUsersAsync()
    {
        var userDataXDocument = await LoadUserDataAsync();

        var users = userDataXDocument.Root.Elements("User")
            .Select(el => new User()
            {
                Id = (int)el.Element("Id"),
                Name = (string)el.Element("Name"),
                Surname = (string)el.Element("Surname"),
                CellphoneNumber = (string)el.Element("CellphoneNumber"),
            }).ToList();

        if (!users.Any()) return null;

        return users;
    }

    public async Task UpdateUserAsync(User user)
    {
        var userDataXDocument = await LoadUserDataAsync();

        var el = userDataXDocument.Root.Elements("User")
            .FirstOrDefault(e => (int)e.Element("Id") == user.Id);

        if (el == null) return;

        el.ReplaceAll(
            new XElement("Id", user.Id),
            new XElement("Name", user.Name),
            new XElement("Surname", user.Surname),
            new XElement("CellphoneNumber", user.CellphoneNumber)
        );

        await SaveUserDataAsync(userDataXDocument);
    }


    public async Task<bool> DeleteUserAsync(int userId)
    {
        var userDataXDocument = await LoadUserDataAsync();
        var el = userDataXDocument.Root.Elements("User")
            .FirstOrDefault(e => (int)e.Element("Id") == userId);

        if (el == null)
        {
            return false;
        }
        
        el.Remove();
        
        await SaveUserDataAsync(userDataXDocument);
        return true;
    }
}