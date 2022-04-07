using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web.Controllers
{
    [Route("project-employee")]
    public class ProjectEmployeeController : Controller
    {
        private readonly IProjectEmployeeService _projectEmployeeService;
        public ProjectEmployeeController(IProjectEmployeeService projectEmployeeService)
        {
            _projectEmployeeService = projectEmployeeService;
        }

        [HttpGet("all")]
        [Route("all")] //second variant of route
        public async Task<IActionResult> GetAll()
        {
            return View(await _projectEmployeeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            return View(await _projectEmployeeService.GetDetails(id));
        }
    }
}
