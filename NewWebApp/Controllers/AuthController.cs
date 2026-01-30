using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Security.Claims;

namespace NewWebApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AuthController(
        IMediator mediator,
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _mediator = mediator;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand cmd)
        => Ok(await _mediator.Send(cmd));

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery query)
        => Ok(await _mediator.Send(query));

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("roles")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
            return BadRequest("Role name is required.");

        if (await _roleManager.RoleExistsAsync(request.RoleName))
            return BadRequest($"Role '{request.RoleName}' already exists.");

        var role = new IdentityRole<Guid>(request.RoleName.Trim());
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });

        return Ok(new { roleName = role.Name });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return NotFound("User not found.");

        if (string.IsNullOrWhiteSpace(request.RoleName))
            return BadRequest("Role name is required.");

        if (!await _roleManager.RoleExistsAsync(request.RoleName.Trim()))
            return BadRequest($"Role '{request.RoleName}' does not exist. Create it first via POST api/auth/roles.");

        var result = await _userManager.AddToRoleAsync(user, request.RoleName.Trim());

        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });

        return Ok(new { userId = user.Id, roleName = request.RoleName });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("users/{id:guid}/claims")]
    public async Task<IActionResult> GetUserClaims(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound("User not found.");

        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var response = new
        {
            userId = user.Id,
            claims = claims.Select(c => new { c.Type, c.Value }).ToList(),
            roles
        };
        return Ok(response);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("users/{id:guid}/claims")]
    public async Task<IActionResult> AddUserClaim(Guid id, [FromBody] AddUserClaimRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound("User not found.");

        if (string.IsNullOrWhiteSpace(request.Type) || string.IsNullOrWhiteSpace(request.Value))
            return BadRequest("Claim type and value are required.");

        var claim = new Claim(request.Type.Trim(), request.Value.Trim());
        var result = await _userManager.AddClaimAsync(user, claim);

        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });

        return Ok(new { userId = user.Id, claim = new { claim.Type, claim.Value } });
    }
}

public record AddRoleRequest(string RoleName);
public record AssignRoleRequest(Guid UserId, string RoleName);
public record AddUserClaimRequest(string Type, string Value);
