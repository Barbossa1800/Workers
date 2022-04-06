using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Controllers
{
    [Route("task-status")]
    public class TaskStatusController : Controller
    {
        readonly WorkerDbContext _db;
        public TaskStatusController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var taskStatus = await _db.TaskStatuses
                .Include(x => x.Employee)
                .Include(x => x.Task)
                .Include(x =>x.StatusTask)
                .ToListAsync();
            return View(taskStatus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var taskStatus = await _db.TaskStatuses
                .Include(x => x.Employee)
                .Include(x => x.StatusTask)
                .Include(x => x.Task)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (taskStatus == null)
            {
                return LocalRedirect("~/task-status/all");
            }
            return View(taskStatus);
        }
    }
}
