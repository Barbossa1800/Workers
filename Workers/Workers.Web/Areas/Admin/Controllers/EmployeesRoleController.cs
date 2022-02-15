using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Area("Admin")]
    [Route("admin/employee-role")]
    [Authorize(Roles = "Admin")]
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

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(EmployeeRole employeeRole)
        {
            await _db.EmployeeRoles.AddAsync(employeeRole);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/employee-role/all");
        }

    }
}
