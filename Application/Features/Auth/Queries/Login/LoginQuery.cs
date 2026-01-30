using MediatR;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<string>;
