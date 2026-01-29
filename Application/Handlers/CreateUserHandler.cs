using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repo;

    public CreateUserHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        await _repo.AddAsync(user);

        return user.Id;
    }
}
