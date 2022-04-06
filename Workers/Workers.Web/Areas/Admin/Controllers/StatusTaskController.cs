using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/status-task")]
    [Authorize(Roles = "Admin")]
    public class StatusTaskController : Controller
    {
        private readonly WorkerDbContext _db;
        public StatusTaskController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Status statusTask)
        {
            await _db.AddAsync(statusTask);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-task/all");
        }
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var statusTask = await _db.Statuses.SingleOrDefaultAsync(x => x.Id == id);
            if (statusTask == null)
            {
                return LocalRedirect("~/status-task/all");
            }
            return View(statusTask);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditStatusTask(Status statusTask)
        {
            var statusTaskFromDb = await _db.Statuses.SingleOrDefaultAsync(x => x.Id == statusTask.Id);
            if (statusTaskFromDb == null)
            {
                return LocalRedirect("~/status-task/all");
            }
            statusTaskFromDb.StatusName = statusTask.StatusName;
            _db.Update(statusTaskFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-task/all");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var statusTask = await _db.Statuses.SingleOrDefaultAsync(x => x.Id == id);
            if (statusTask == null)
            {
                return LocalRedirect("~/status-task/all");
            }
            return View(statusTask);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTask(Status statusTask)
        {
            var statusTaskFromDb = await _db.Statuses.SingleOrDefaultAsync(x => x.Id == statusTask.Id);
            if (statusTaskFromDb == null)
            {
                return LocalRedirect("~/status-task/all");
            }
            _db.Remove(statusTaskFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-task/all");
        }
    }
}
