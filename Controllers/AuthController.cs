using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Data;
using System.Linq;

namespace TaskManagerApi.Controllers
{
   [ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _ctx;
    public AuthController(AppDbContext ctx) { _ctx = ctx; }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User u)
    {
        if (_ctx.Users.Any(x => x.Email == u.Email))
            return Ok(new { success = false });
        _ctx.Users.Add(u);
        _ctx.SaveChanges();
        return Ok(new { success = true });
    }

    [HttpPost("login")]
public IActionResult Login([FromBody] User login)
{
    var u = _ctx.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
    return Ok(new { success = u != null, user = u });
}

}

}
