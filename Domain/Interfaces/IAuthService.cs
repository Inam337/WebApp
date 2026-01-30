public interface IAuthService
{
    Task<Guid> Register(string email, string password, string fullName);
    Task<string> Login(string email, string password);
}