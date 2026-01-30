using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewWebApp.Controllers;

[Authorize(Roles = "Admin,Manager")]
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Admin or Manager access granted.", user = User.Identity?.Name });
    }
}
