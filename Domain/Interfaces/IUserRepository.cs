using Domain.Entities;
namespace Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<List<User>> GetAllAsync();
}