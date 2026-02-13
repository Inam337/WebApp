using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetUserClaims : IRequestHandler<GetUserClaimsQuery, object>
    {
        private readonly UserManager<User> _userManager;

        public GetUserClaims(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<object> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                ?? throw new Exception("User not found");

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new
            {
                userId = user.Id,
                claims = claims.Select(c => new { c.Type, c.Value }),
                roles
            };
        }
    }

}
