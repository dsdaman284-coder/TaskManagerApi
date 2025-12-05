using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Users.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updated)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Username = updated.Username;
            user.Email = updated.Email;
            user.Password = updated.Password;
            user.Role = updated.Role;

            _context.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(new { success = true });
        }
    }
}
