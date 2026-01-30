using MediatR;
using Domain.Entities;
using Domain.Extensions;
using Domain.Interfaces;
using Application.Commands;

namespace Application.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        if (user == null)
            throw new Exception("User not found");

        // ✅ Use extension method
        user.Update(request.Name, request.Email);

        await _repository.UpdateAsync(user);
        return user;
    }
}
