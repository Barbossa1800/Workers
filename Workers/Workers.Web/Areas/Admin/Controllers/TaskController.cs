using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/task")]
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        private readonly WorkerDbContext _db;

        public TaskController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await _db.Projects.ToListAsync();
            ViewBag.StatusTasks = await _db.Statuses.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Domain.Models.Task task)
        {
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _db.Tasks.Include(x => x.Project).Include(x => x.TaskStatuses).SingleOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return LocalRedirect("~/task/all");
            }

            var projects = await _db.Projects.ToArrayAsync();

            ViewBag.Projects = projects;

            return View(task);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditTask(Domain.Models.Task task)
        {
            var taskFromDb = await _db.Tasks.Include(x => x.Project).Include(x => x.TaskStatuses).SingleOrDefaultAsync(x => x.Id == task.Id);
            if (task == null)
            {
                return LocalRedirect("~/task/all");
            }

            taskFromDb.DeadLine = task.DeadLine;
            taskFromDb.Name = task.Name;
            taskFromDb.PercentOfCompleted = task.PercentOfCompleted;
            taskFromDb.Description = task.Description;
            taskFromDb.ProjectId = task.ProjectId;

            _db.Update(taskFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task/all");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.Tasks.Include(x => x.Project).Include(x => x.TaskStatuses).SingleOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return LocalRedirect("~/task/all");
            }
            return View(task);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTask(Domain.Models.Task task)
        {
            var taskFromDb = await _db.Tasks
                .Include(x => x.Project)
                .Include(x => x.TaskStatuses)
                .SingleOrDefaultAsync(x => x.Id == task.Id);
            if (task == null)
            {
                return LocalRedirect("~/task/all");
            }
            _db.Remove(taskFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task/all");
        }
    }
}
