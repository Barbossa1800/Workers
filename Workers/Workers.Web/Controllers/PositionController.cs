using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Route("position")]
    public class PositionController : Controller
    {
        WorkerDbContext _db;
        public PositionController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var positions = await _db.Positions.ToListAsync();
            return View(positions);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Position position)
        {
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/position/all");
        }
    }
}
