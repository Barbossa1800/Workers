using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Controllers
{
    [Route("status-project")]
    public class StatusProjectController : Controller
    {
        private readonly WorkerDbContext _db;
        public StatusProjectController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var statuSpoject = await _db.StatusProjects.ToListAsync();
            return View(statuSpoject);
        }

       
    }
}
