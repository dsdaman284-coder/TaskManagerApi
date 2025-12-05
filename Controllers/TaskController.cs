using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            return Ok(task);
        }


        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(new { success = true, message = "Task created" });
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem updatedTask)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;
            task.DueDate = updatedTask.DueDate;
            task.UserId = updatedTask.UserId;

            _context.SaveChanges();
            return Ok(new { success = true, message = "Task updated" });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok(new { success = true, message = "Task deleted" });
        }
    }
}
