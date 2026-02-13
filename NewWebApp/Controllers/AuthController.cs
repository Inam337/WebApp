using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Security.Claims;
using Application.Commands;
using Application.Queries;

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

    [HttpPost("roles")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
         => Ok(await _mediator.Send(new CreateRoleCommand(request.RoleName)));

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        await _mediator.Send(new AssignRoleCommand(request.UserId, request.RoleName));
        return Ok();
    }

    [HttpGet("users/{id:guid}/claims")]
    public async Task<IActionResult> GetUserClaims(Guid id)
        => Ok(await _mediator.Send(new GetUserClaimsQuery(id)));

    [HttpPost("users/{id:guid}/claims")]
    public async Task<IActionResult> AddUserClaim(Guid id, [FromBody] AddUserClaimRequest request)
    {
        await _mediator.Send(new AddUserClaimCommand(id, request.Type, request.Value));
        return Ok();
    }
}

public record AddRoleRequest(string RoleName);
public record AssignRoleRequest(Guid UserId, string RoleName);
public record AddUserClaimRequest(string Type, string Value);
