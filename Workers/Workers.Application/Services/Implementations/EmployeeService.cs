using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Domain.Models;
using Workers.Infrastructure.Data.Context;
using Extensions.Password;

namespace Workers.Application.Services.Implementations
{

    public class EmployeeService : IEmployeeService
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

        public Employee Create() //attention
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

        public async Task<Employee> Edit(int id)
        {
            return await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            var userFromDb = await _db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
            if (userFromDb == null)
                throw new System.Exception("Employy by id not found");
            userFromDb.FirstName = employee.FirstName;
            userFromDb.LastName = employee.LastName;
            // userFromDb.Login = employee.Login;
            userFromDb.Email = employee.Email;
            await _db.SaveChangesAsync();
            return userFromDb;

        }

        public async Task<Employee> Delete(int id)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                throw new System.Exception("Employy by id not found");
            return employee;
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            var employeeForDelete = await _db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
            if (employee == null)
            {
                throw new System.Exception("Employy by id not found");
            }
            _db.Remove(employeeForDelete);
            await _db.SaveChangesAsync();
            return employeeForDelete;
        }
    }
}
