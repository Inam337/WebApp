using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class LoginHandler
    {
        private readonly IAuthService _auth;

        public LoginHandler(IAuthService auth)
        {
            _auth = auth;
        }

        public Task<string> Handle(LoginQuery request, CancellationToken ct)
        {
            return _auth.Login(request.Email, request.Password);
        }
    }

}



