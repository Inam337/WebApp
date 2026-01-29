using MediatR;
using Domain.Entities;

namespace Application.Queries;

public record GetUsersQuery : IRequest<List<User>>;