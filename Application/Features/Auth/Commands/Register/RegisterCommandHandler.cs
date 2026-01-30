using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IAuthService _auth;

    public RegisterCommandHandler(IAuthService auth)
    {
        _auth = auth;
    }

    public Task<Guid> Handle(RegisterCommand request, CancellationToken ct)
        => _auth.Register(request.Email, request.Password, request.FullName);
}
