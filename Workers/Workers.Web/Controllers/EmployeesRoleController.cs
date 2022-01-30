using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;

namespace Workers.Web.Controllers
{
    [Route("employee-role")]
    public class EmployeesRoleController : Controller
    {
        private readonly WorkerDbContext _db;
        public EmployeesRoleController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employeeRole = await _db.EmployeeRoles.ToListAsync();
            return View();
        }

    }
}
