using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Controllers
{
    [Route("status-task")]
    public class StatusTaskController : Controller
    {
        /*This controller for Status (statuses) logic and table in DB Statuses. Is not a TaskStatus in DB. 
         NOT TO BE CONFUSED*/
        private readonly WorkerDbContext _db;
        public StatusTaskController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var statusTasks = await _db.Statuses.ToListAsync();
            return View(statusTasks);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var statusTasks = await _db.Statuses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (statusTasks == null)
            {
                return LocalRedirect("~/status-task/all");
            }
            return View(statusTasks);
        }
    }
}
