using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Controllers
{
    [Route("position")]
    public class PositionController : Controller
    {
        private readonly WorkerDbContext _db;
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
