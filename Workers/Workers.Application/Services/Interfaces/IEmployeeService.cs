using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Domain.Models;

namespace Workers.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        #region StableWorks_Example
        Task<List<Employee>> GetAll(); 
        Task<Employee> GetDetails(int id);
        #endregion
        Employee Create();
        Task<Employee> Create(Employee employee);
    }
}
