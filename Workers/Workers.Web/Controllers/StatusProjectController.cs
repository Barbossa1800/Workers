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
    [Route("status-project")]
    public class StatusProjectController : Controller
    {
        WorkerDbContext _db;
        public StatusProjectController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var statuSpoject = await _db.StatusProjects.ToListAsync();
            return View(statuSpoject);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StatusProject statusProject)
        {
             _db.StatusProjects.Update(statusProject);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return LocalRedirect("~/status-project/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var statusProject = await _db.StatusProjects.SingleOrDefaultAsync(x =>x.Id == id);
            return View(statusProject);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditStatusProject(StatusProject statusProject)
        {
            var statusProjectFromDb = await _db.StatusProjects.SingleOrDefaultAsync(x => x.Id == statusProject.Id);
            if(statusProjectFromDb == null)
                return LocalRedirect("~/status-project/all");

            statusProjectFromDb.Status = statusProject.Status;
            _db.Update(statusProjectFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-project/all");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var statusProjectFromDb = await _db.StatusProjects.SingleOrDefaultAsync(x => x.Id == id);
            if (statusProjectFromDb == null)
                return LocalRedirect("~/status-project/all");
            return View(statusProjectFromDb);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteStatusproject(StatusProject statusProject)
        {
            var statusProjectFromDb = await _db.StatusProjects.SingleOrDefaultAsync(x => x.Id == statusProject.Id);
            if (statusProjectFromDb == null)
                return LocalRedirect("~/status-project/all");
            _db.Remove(statusProjectFromDb);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/status-project/all");
        }
    }
}
