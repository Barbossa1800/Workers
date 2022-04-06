using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

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
            var project = await _db.Projects.Include(x => x.StatusProject).ToListAsync();
            return View(project);
        }

       

    }
}
