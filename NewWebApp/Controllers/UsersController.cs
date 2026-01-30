using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries;
using Application.Queries;
using Domain.Entities;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(CreateUserCommand command)
    {
        var user = await _mediator.Send(command);
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }
}
