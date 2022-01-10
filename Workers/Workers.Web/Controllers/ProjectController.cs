using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private readonly WorkerDbContext _db;
        public ProjectController(WorkerDbContext db)
        {
            _db = db;
        }

        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var project = await _db.Projects.ToListAsync();
            return View(project);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/project/all");
        }


    }
}
