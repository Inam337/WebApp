using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using Application.Queries;

namespace Application.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
