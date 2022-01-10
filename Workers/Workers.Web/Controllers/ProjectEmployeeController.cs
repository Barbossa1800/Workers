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
    [Route("project-employee")]
    public class ProjectEmployeeController : Controller
    {
        private readonly WorkerDbContext _db;
        public ProjectEmployeeController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        [Route("all")] //second variant of route
        public async Task<IActionResult> GetAll()
        {
            var projectemployee = await _db.ProjectEmployees.Include(x => x.Employee).Include(x => x.Project).Include(x => x.Position).ToListAsync();
            return View(projectemployee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var pe = await _db.ProjectEmployees.Include(x => x.Employee).Include(x => x.Project).Include(x => x.Position).SingleOrDefaultAsync(x => x.Id == id);
            return View(pe);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var pe = await _db.ProjectEmployees.Include(x=>x.Employee).SingleOrDefaultAsync(x => x.Id == id);
            if (pe is null)
                return LocalRedirect(Url.Action("GetAll", "ProjectEmployee"));

            var projects = await _db.Projects.ToListAsync();
            var positions = await _db.Positions.ToListAsync();

            ViewBag.Projects = projects;
            ViewBag.Positions = positions;

            return View(pe);
        }

        [HttpPost("edit")] //for create project emp likely this
        public async Task<IActionResult> EditProjEmp(ProjectEmployee projectEmployee)
        {
            var peFromDb = await _db.ProjectEmployees.SingleOrDefaultAsync(x => x.Id == projectEmployee.Id);
            if (peFromDb is null)
                return LocalRedirect(Url.Action("GetAll", "ProjectEmployee"));

            peFromDb.PositionId = projectEmployee.PositionId;
            peFromDb.ProjectId = projectEmployee.ProjectId;

            _db.ProjectEmployees.Update(peFromDb);
            await _db.SaveChangesAsync();

            return LocalRedirect(Url.Action("GetDetails", "ProjectEmployee", new { id = peFromDb.Id }));
        }
    }
}
