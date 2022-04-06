using System.Collections.Generic;
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

        //admin method
        Employee Create(); //attention!
        Task<Employee> Create(Employee employee);
        Task<Employee> Edit(int id);
        Task<Employee> EditEmployee(Employee employee);
        Task<Employee> Delete(int id);
        Task<Employee> DeleteEmployee(Employee employee);
    }
}
