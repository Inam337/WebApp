using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleName = request.RoleName.Trim();

            if (await _roleManager.RoleExistsAsync(roleName))
                throw new Exception($"Role '{roleName}' already exists.");

            var role = new IdentityRole<Guid>(roleName);

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));

            return role.Name!;
        }
    }

}
