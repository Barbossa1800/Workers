using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Route("status-task")]
    public class StatusTaskController : Controller
    {
        private readonly WorkerDbContext _db;
        public StatusTaskController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var statusTask = await _db.StatusTasks.ToListAsync();
            return View(statusTask);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StatusTask statusTask)
        {
            await _db.AddAsync(statusTask);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-task/all");
        }
    }
}
