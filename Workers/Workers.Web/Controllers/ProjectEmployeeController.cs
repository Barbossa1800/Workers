using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;

namespace Workers.Web.Controllers
{
    [Route("projectemployee")]
    public class ProjectEmployeeController : Controller
    {
        private readonly WorkerDbContext _db;
        public ProjectEmployeeController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var projectemployee = await _db.ProjectEmployees.ToListAsync();
            return View(projectemployee);
        }
    }
}
