using MediatR;

public record RegisterCommand(
    string Email,
    string Password,
    string FullName
) : IRequest<Guid>;
