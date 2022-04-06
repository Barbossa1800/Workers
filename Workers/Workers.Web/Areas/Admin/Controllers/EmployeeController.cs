using Extensions.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Domain.Models;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/employee")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _employeeService.Create(employee);
            return LocalRedirect("~/employee/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _employeeService.Edit(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            await _employeeService.EditEmployee(employee);
            return LocalRedirect("~/employee/all");
        }
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _employeeService.Delete(id));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteEmployee(Employee employee)
        {
            await _employeeService.DeleteEmployee(employee);
            return LocalRedirect("~/employee/all");
        }
    }
}
