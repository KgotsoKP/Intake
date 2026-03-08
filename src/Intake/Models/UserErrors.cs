using Intake.Common;

namespace Intake.Models;

public static class UserErrors
{
    public static Error FailedToCreateError => new(
        "User.FailedToCreateError", "Cannot create user");
    public static Error NotFound(int id) => new(
        "User.NotFound", $"The user with Id '{id}' was not found");
}