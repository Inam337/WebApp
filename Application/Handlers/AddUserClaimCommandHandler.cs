using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand>
    {
        private readonly UserManager<User> _userManager;

        public AddUserClaimCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                ?? throw new Exception("User not found");

            var claim = new Claim(request.Type.Trim(), request.Value.Trim());

            var result = await _userManager.AddClaimAsync(user, claim);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
    }

}
