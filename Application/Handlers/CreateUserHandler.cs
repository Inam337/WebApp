using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _repo;

    public CreateUserHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email
        };

        await _repo.AddAsync(user);

        return user;
    }
}
