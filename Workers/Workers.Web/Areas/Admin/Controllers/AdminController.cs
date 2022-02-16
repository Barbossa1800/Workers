using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;

namespace Workers.Web.Areas.Admin
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //ещё список всех пользоватлей, и круд с ними
        //policy - гибче
        private readonly WorkerDbContext _db;
        public AdminController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _db.Roles.ToListAsync();
            return View(roles);
        }
    }
}
