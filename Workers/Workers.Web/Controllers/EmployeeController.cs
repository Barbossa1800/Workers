using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;

namespace Workers.Web.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #region StableWorks_Example
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() //rename -> GetAllEmployees mb + "Async"
        {

            return View(await _employeeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id) //rename -> GetEmployeeById mb + "Async"
        {
            return View(await _employeeService.GetDetails(id));
        }
        #endregion
    }
}
