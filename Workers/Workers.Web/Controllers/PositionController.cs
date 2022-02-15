using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;

namespace Workers.Web.Controllers
{
    [Route("position")]
    public class PositionController : Controller
    {
        WorkerDbContext _db;
        public PositionController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var positions = await _db.Positions.ToListAsync();
            return View(positions);
        }

       

    }
}
