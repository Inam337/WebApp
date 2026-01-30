using Domain.Entities;

namespace Domain.Extensions;

public static class UserExtensions
{
    public static void Update(this User user, string name, string email)
    {
        user.Email = email;
    }
}