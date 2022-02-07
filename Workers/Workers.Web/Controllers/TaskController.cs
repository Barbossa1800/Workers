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
        [Authorize(Policy = "ForEmail")]
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

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await _db.Projects.ToListAsync();
            ViewBag.StatusTasks = await _db.Statuses.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Infrastructure.Models.Task task)
        {
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _db.Tasks.Include(x => x.Project).Include(x => x.TaskStatuses).SingleOrDefaultAsync(x => x.Id == id);
            if(task == null)
            {
                return LocalRedirect("~/task/all");
            }

            var projects = await _db.Projects.ToArrayAsync();

            ViewBag.Projects = projects;

            return View(task);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditTask(Infrastructure.Models.Task task)
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
        public async Task<IActionResult> Delete (int id)
        {
            var task = await _db.Tasks.Include(x => x.Project).Include(x =>x.TaskStatuses).SingleOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return LocalRedirect("~/task/all");
            }
            return View(task);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTask(Infrastructure.Models.Task task)
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
