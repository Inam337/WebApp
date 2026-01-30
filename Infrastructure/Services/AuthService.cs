using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _users;
    private readonly SignInManager<User> _signIn;
    private readonly IJwtService _jwt;

    public AuthService(UserManager<User> users, SignInManager<User> signIn, IJwtService jwt)
    {
        _users = users;
        _signIn = signIn;
        _jwt = jwt;
    }

    public async Task<Guid> Register(string email, string password, string fullName)
    {
        var user = new User
        {
            Email = email,
            UserName = email,
            FullName = fullName
        };

        var result = await _users.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        // Assign default role "User"
        await _users.AddToRoleAsync(user, "User");

        return user.Id;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _users.FindByEmailAsync(email)
            ?? throw new UnauthorizedAccessException();

        var result = await _signIn.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException();

        return await _jwt.GenerateAsync(user);
    }
}
