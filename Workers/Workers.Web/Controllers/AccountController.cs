using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Workers.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private WorkerDbContext _db;
        public AccountController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EmployeeRegister model)
        {
            if (ModelState.IsValid)
            {
                Employee user = await _db.Employees.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new Employee
                    {
                        Email = model.Email,
                        PasswordHash = model.PasswordHash,
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    _db.Employees.Add(user);
                    await _db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(EmployeeLogin model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _db.Employees.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.PasswordHash);
                if (employee != null)
                {
                    await Authenticate(employee); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
                return LocalRedirect("~/");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("~/");
        }

        private async System.Threading.Tasks.Task Authenticate(Employee employee)
        {
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimsIdentity.DefaultNameClaimType, employee.Email), //зачем этот клеим? базовый для аутентификации??
                new System.Security.Claims.Claim(ClaimTypes.Email, employee.Email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

}
