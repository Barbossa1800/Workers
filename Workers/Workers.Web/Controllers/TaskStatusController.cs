using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

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

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = new SelectList(await _db.Employees.Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync(), "Id", "FullName");
            ViewBag.Statuses = new SelectList(await _db.Statuses.Select(x => new Status
            {
                Id = x.Id,
                StatusName = x.StatusName
            }).ToListAsync(), "Id", "StatusName");
            ViewBag.Tasks = new SelectList(await _db.Tasks.Select(x => new Infrastructure.Models.Task
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Infrastructure.Models.TaskStatus taskStatus)
        {
            await _db.TaskStatuses.AddAsync(taskStatus);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/task-status/all");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var taskStatus = await _db.TaskStatuses
                .Include(x => x.Employee)
                .Include(x => x.StatusTask)
                .Include(x => x.Task)
                .SingleOrDefaultAsync(x =>x.Id == id);
            if(taskStatus == null)
            {
                return LocalRedirect("~/task-status/all");
            }
            return View(taskStatus);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
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

            var statuses = new SelectList(await _db.Statuses.Select(x => new Status
            {
                Id = x.Id,
                StatusName = x.StatusName
            }).ToListAsync(), "Id", "StatusName");
            var emploees = new SelectList(await _db.Employees.Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync(), "Id", "FullName");
            var tasks = new SelectList(await _db.Tasks.Select(x => new Infrastructure.Models.Task
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(), "Id", "Name");

            ViewBag.Statuses = statuses;
            ViewBag.Employees = emploees;
            ViewBag.Tasks = tasks;

            return View(taskStatus);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditTaskStatus(Infrastructure.Models.TaskStatus taskStatus)
        {
            var taskStatusFromDb = await _db.TaskStatuses
               .Include(x => x.Employee)
               .Include(x => x.StatusTask)
               .Include(x => x.Task)
               .SingleOrDefaultAsync(x => x.Id == taskStatus.Id);

            if (taskStatusFromDb == null)
            {
                return LocalRedirect("~/task-status/all");
            }

            taskStatusFromDb.StatusTaskId = taskStatus.StatusTaskId;
            taskStatusFromDb.EmployeeId = taskStatus.EmployeeId;
            taskStatusFromDb.TaskId = taskStatus.TaskId;

            _db.TaskStatuses.Update(taskStatusFromDb);
            await _db.SaveChangesAsync();

            return LocalRedirect("~/task-status/all");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskStatus = await _db.TaskStatuses
                .Include(x => x.Employee)
                .Include(x => x.Task)
                .Include(x => x.StatusTask)
                .SingleOrDefaultAsync(x =>x.Id == id);

            if (taskStatus == null)
            {
                return LocalRedirect("~/task-status/all");
            }

            return View(taskStatus);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTaskStatus(Infrastructure.Models.TaskStatus taskStatus)
        {
            var taskStatusForDelete = await _db.TaskStatuses
                .Include(x => x.Employee)
                .Include(x => x.Task)
                .Include(x => x.StatusTask)
                .SingleOrDefaultAsync(x => x.Id == taskStatus.Id);

            if (taskStatusForDelete == null)
            {
                return LocalRedirect("~/task-status/all");
            }

             _db.TaskStatuses.Remove(taskStatusForDelete);
            await _db.SaveChangesAsync();

            return LocalRedirect("~/task-status/all");

        }
    }
}
