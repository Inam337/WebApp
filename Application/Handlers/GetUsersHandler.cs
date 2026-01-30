using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using Application.Queries;

namespace Application.Handlers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
