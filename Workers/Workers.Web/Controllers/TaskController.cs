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
        public  async Task<IActionResult> GetAll()
        {
            var tasks = await _db.Tasks.Include(x => x.Project).Include(x =>x.InfoStatuses).ToListAsync();
            return View(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var task = await _db.Tasks.Include(x => x.InfoStatuses).ThenInclude(x => x.StatusTask).ToListAsync();
            return View(task);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await _db.Projects.ToListAsync();
            ViewBag.StatusTasks = await _db.StatusTasks.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Infrastructure.Models.Task task)
        {
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task/all");
        }


    }
}
