using MediatR;
using Domain.Entities;

namespace Application.Commands;

public record DeleteUserCommand(Guid Id) : IRequest;
