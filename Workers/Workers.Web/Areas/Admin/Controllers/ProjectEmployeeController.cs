using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Workers.Infrastructure.Data.Context;
using Workers.Domain.Models;
using Workers.Application.Services.Interfaces;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/project-employee")]
    [Authorize(Roles = "Admin")]
    public class ProjectEmployeeController : Controller
    {
        private readonly IProjectEmployeeService _projectEmployeeService;
        private readonly WorkerDbContext _db;
        public ProjectEmployeeController(IProjectEmployeeService projectEmployeeService, WorkerDbContext db) //КОСТЫЛЬ (для метода Create [Get] 
        {
            _projectEmployeeService = projectEmployeeService;
            _db = db;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create() // I don't understand how to take this method to Services
        {
            ViewBag.Projects = await _db.Projects.Select(x => new Project
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            ViewBag.Employees = await _db.Employees.Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync();
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProjectEmployee projectEmployee)
        {
            await _projectEmployeeService.Create(projectEmployee);
            return LocalRedirect("~/project-employee/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var pe = await _db.ProjectEmployees.Include(x => x.Employee).SingleOrDefaultAsync(x => x.Id == id);
            if (pe is null)
                return LocalRedirect(Url.Action("GetAll", "ProjectEmployee"));

            var projects = await _db.Projects.ToListAsync();
            var positions = await _db.Positions.ToListAsync();

            ViewBag.Projects = projects;
            ViewBag.Positions = positions;

            return View(pe);
        } // I don't understand how to take this method to Services

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
        } // I don't understand how to take this method to Services

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _projectEmployeeService.Delete(id));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConfirmed(ProjectEmployee projectEmployee)
        {
            await _projectEmployeeService.DeleteConfirmed(projectEmployee);
            return LocalRedirect("~/project-employee/all");
        }
    }
}
