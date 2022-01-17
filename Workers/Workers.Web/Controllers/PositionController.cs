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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var position = await _db.Positions.AsNoTracking().SingleOrDefaultAsync(x =>x.Id == id);
            return View(position);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditPosition(Position position)
        {
            var positionFromDb = await _db.Positions.AsNoTracking().SingleOrDefaultAsync(x => x.Id == position.Id);
            if(position == null)
                return LocalRedirect("~/position/all");
             _db.Update(positionFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/position/all");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var positionFromDb = await _db.Positions.SingleOrDefaultAsync(x => x.Id == id);
            if (positionFromDb == null)
                return LocalRedirect("~/position/all"); 
            return View();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePosition(Position position)
        {
            var positionFromDb = await _db.Positions.SingleOrDefaultAsync(x => x.Id == position.Id);
            if (positionFromDb == null)
                return LocalRedirect("~/position/all");
             _db.Remove(positionFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/position/all");
        }

    }
}
