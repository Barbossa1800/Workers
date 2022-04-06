using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;

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
            var employeeRole = await _db.EmployeeRoles
                .Include(x => x.Role)
                .Include(x => x.Employee)
                .ToListAsync();


            return View(employeeRole);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await _db.Employees.Select(x => new Employee
            {
                Id = x.Id,
                Email = x.Email
            }).ToListAsync();
            ViewBag.Roles = await _db.Roles.Select(x => new Role
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View();
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeRole = await _db.EmployeeRoles.Include(x => x.Employee).Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);
            if (employeeRole == null)
            {
                return LocalRedirect("~/admin/employee-role/all");
            }

            var employees = await _db.Employees.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            ViewBag.Employees = employees;
            ViewBag.Roles = roles;

            return View(employeeRole);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditEmployeesRole(EmployeeRole employeeRoleToUpdate)
        {
            var currentEmployeeRole = await _db.EmployeeRoles
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == employeeRoleToUpdate.Id);

            if (currentEmployeeRole == null)
            {
                return LocalRedirect("~/admin/employee-role/all");
            }

            currentEmployeeRole.EmployeeId = employeeRoleToUpdate.EmployeeId;
            currentEmployeeRole.RoleId = employeeRoleToUpdate.RoleId;

            _db.EmployeeRoles.Update(currentEmployeeRole);
            await _db.SaveChangesAsync();

            if (Convert.ToInt32(HttpContext.User.Claims.First(s => s.Type == "Id").Value) == currentEmployeeRole.Employee.Id)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return LocalRedirect("~/account/login");
            }
            return LocalRedirect("~/admin/employee-role/all");
        }
    }
}
