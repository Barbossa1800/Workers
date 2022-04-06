using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/project")]
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly WorkerDbContext _db;
        public ProjectController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.StatusProjects = await _db.StatusProjects.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/project/all");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var project = await _db.Projects.Include(x => x.StatusProject).SingleOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return LocalRedirect("~/project/all");
            }
            return View(project);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _db.Projects.Include(x => x.StatusProject).SingleOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return LocalRedirect("~/project/all");
            }

            var statusProjects = await _db.StatusProjects.ToListAsync();

            ViewBag.StatusProjects = statusProjects;
            return View(project);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditProject(Project project)
        {
            var projectFromDb = await _db.Projects.Include(x => x.StatusProject).SingleOrDefaultAsync(x => x.Id == project.Id);
            if (projectFromDb == null)
            {
                return LocalRedirect("~/project/all");
            }

            projectFromDb.StatusProjectId = project.StatusProjectId;
            projectFromDb.Name = project.Name;
            projectFromDb.StartAt = project.StartAt;
            projectFromDb.EndAt = project.EndAt;
            projectFromDb.StatusProjectId = project.StatusProjectId;

            _db.Projects.Update(projectFromDb);
            await _db.SaveChangesAsync();

            return LocalRedirect(Url.Action("GetDetails", "Project", new { id = projectFromDb.Id }));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.Include(x => x.StatusProject).SingleOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return LocalRedirect("~/project/all");
            }
            return View(project);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConfirmed(Project project)
        {
            var projectFromDelete = await _db.Projects.Include(x => x.StatusProject).SingleOrDefaultAsync(x => x.Id == project.Id);
            if (project == null)
            {
                return LocalRedirect("~/project/all");
            }
            _db.Remove(projectFromDelete);
            _db.SaveChanges();
            return LocalRedirect("~/project/all");
        }
    }
}
