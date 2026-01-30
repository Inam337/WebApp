using MediatR;

public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IAuthService _auth;

    public LoginQueryHandler(IAuthService auth)
    {
        _auth = auth;
    }

    public Task<string> Handle(LoginQuery request, CancellationToken ct)
        => _auth.Login(request.Email, request.Password);
}
