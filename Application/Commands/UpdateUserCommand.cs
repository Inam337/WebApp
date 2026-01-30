using MediatR;
using Domain.Entities;

namespace Application.Commands;

public record UpdateUserCommand(Guid Id, string Name, string Email) : IRequest<User>;
