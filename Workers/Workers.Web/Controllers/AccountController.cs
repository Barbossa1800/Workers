using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Extensions.Password;
using Workers.Web.Infrastructure.Constants;
using System.Linq;

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
                        PasswordHash = model.PasswordHash.GeneratePasswordHash(),
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    _db.Employees.Add(user);
                    await _db.SaveChangesAsync();

                    var empRole = new EmployeeRole
                    {
                        EmployeeId = user.Id,
                        RoleId = (await _db.Roles.AsNoTracking().Select(c => new Role { Id = c.Id }).FirstOrDefaultAsync(x => x.Name == CustomRole.Employee)).Id
                    };

                    await _db.EmployeeRoles.AddAsync(empRole);
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
                Employee employee = await _db.Employees.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (employee != null)
                {
                    if (!model.PasswordHash.VerifyPasswordHash(employee.PasswordHash))
                    {
                        ModelState.AddModelError("", "Некорректный логин и(или) пароль");
                        return View(model);
                    }

                    await Authenticate(employee); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Пользователя не существует");
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
            var userRole = await _db.EmployeeRoles.AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(s => s.EmployeeId == employee.Id);
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimsIdentity.DefaultNameClaimType, employee.Email), //зачем этот клеим? базовый для аутентификации??
                //new System.Security.Claims.Claim(ClaimTypes.Email, employee.Email),
                new System.Security.Claims.Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.Role.Name) //связь на роль нужна...
                //new System.Security.Claims.Claim("email", employee.Email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

}
