using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;

namespace Workers.Application.Services.Implementations
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly WorkerDbContext _db;
        public ProjectEmployeeService(WorkerDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProjectEmployee>> GetAll()
        {
            return await _db.ProjectEmployees
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .ToListAsync();
        }

        public async Task<ProjectEmployee> GetDetails(int id)
        {
            var projectEmployee = await _db.ProjectEmployees
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (projectEmployee == null)
            {
                throw new System.Exception("Project Employee by id not found");/*LocalRedirect("~/project-employee/all");*/
            }
            return projectEmployee;
        }

        //public async Task<List<ProjectEmployee>> Create()
        //{
        //    return await _db.Positions.ToListAsync();

        //}

        public async Task<ProjectEmployee> Create(ProjectEmployee projectEmployee)
        {
            await _db.ProjectEmployees.AddAsync(projectEmployee);
            await _db.SaveChangesAsync();
            return projectEmployee;
        }

        //public Task<ProjectEmployee> Edit(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ProjectEmployee> EditProjEmp(ProjectEmployee projectEmployee)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ProjectEmployee> Delete(int id)
        {
            var projectEmployeeFromDb = await _db.ProjectEmployees
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (projectEmployeeFromDb is null)
                throw new Exception("Project Employee by id not found");
            return projectEmployeeFromDb;
        }

        public async Task<ProjectEmployee> DeleteConfirmed(ProjectEmployee projectEmployee)
        {
            var employeeFromDelete = await _db.ProjectEmployees.SingleOrDefaultAsync(x => x.Id == projectEmployee.Id);
            if (employeeFromDelete is null)
                throw new Exception("Project Employee by id not found");
            _db.Remove(employeeFromDelete);
            _db.SaveChanges();
            return employeeFromDelete;
        }
    }
}
