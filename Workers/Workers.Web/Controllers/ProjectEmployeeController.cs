using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;

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
            var projectemployee = await _db.ProjectEmployees
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .ToListAsync();
            return View(projectemployee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var pe = await _db.ProjectEmployees
                .Include(x => x.Employee)
                .Include(x => x.Project) 
                .Include(x => x.Position)
                .SingleOrDefaultAsync(x => x.Id == id);
            if(pe == null)
            {
                return LocalRedirect("~/project-employee/all");
            }
            return View(pe);
        }

       
    }
}
