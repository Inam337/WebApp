using MediatR;
using Domain.Entities;

namespace Application.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;
