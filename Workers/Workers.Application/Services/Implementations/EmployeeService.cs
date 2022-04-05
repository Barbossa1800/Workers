using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;
using Extensions.Password;

namespace Workers.Application.Services.Implementations
{

    class EmployeeService : IEmployeeService
    {
        private readonly WorkerDbContext _db;
        public EmployeeService(WorkerDbContext db)
        {
            _db = db;
        }
        #region StableWorks_Example
        public async Task<List<Employee>> GetAll()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> GetDetails(int id)
        {
            var employee = await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                throw new System.Exception("Employy by id not found");
            return employee;
        }
        #endregion

        public Employee Create()
        {
            return _db.Employees.Find();
        }

        public async Task<Employee> Create(Employee employee)
        {
            employee.PasswordHash = employee.PasswordHash.GeneratePasswordHash();//генерация хеша пароля. VerifyPasswordHash - проверка
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        
    }
}
