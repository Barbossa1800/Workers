using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly WorkerDbContext _db;

        public TaskController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _db.Tasks.Include(x => x.Project).Include(x => x.TaskStatuses).ToListAsync();
            return View(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var task = await _db.Tasks.Include(x => x.TaskStatuses).ThenInclude(x => x.StatusTask).SingleOrDefaultAsync(x =>x.Id == id);
            return View(task);
        }
    }
}
