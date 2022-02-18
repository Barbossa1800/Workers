using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;
using Extensions.Password;

namespace Workers.Web.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly WorkerDbContext _db;
        public EmployeeController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _db.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var employee = await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                return LocalRedirect("~/employee/all");
            return View(employee);
        }
    }
}
