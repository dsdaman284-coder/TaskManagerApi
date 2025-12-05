using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Data;
using System.Linq;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public AdminController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _ctx.Users.ToList();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _ctx.Users.Find(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost("users")]
        public IActionResult CreateUser(User user)
        {
            if (_ctx.Users.Any(x => x.Email == user.Email))
                return BadRequest("User already exists");
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = _ctx.Users.Find(id);
            if (user == null) return NotFound();

            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.Username = updatedUser.Username;
            user.Role = updatedUser.Role;

            _ctx.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _ctx.Users.Find(id);
            if (user == null) return NotFound();

            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
            return Ok(new { success = true });
        }
    }
}
