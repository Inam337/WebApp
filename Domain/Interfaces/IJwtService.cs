using Domain.Entities;

namespace Domain.Interfaces;

public interface IJwtService
{
    Task<string> GenerateAsync(User user, CancellationToken cancellationToken = default);
}
