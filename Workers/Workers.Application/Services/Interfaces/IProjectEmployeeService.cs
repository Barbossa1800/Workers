using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Domain.Models;

namespace Workers.Application.Services.Interfaces
{
    public interface IProjectEmployeeService
    {
        Task<List<ProjectEmployee>> GetAll();
        Task<ProjectEmployee> GetDetails(int id);
       // Task<List<ProjectEmployee>> Create();
        Task<ProjectEmployee> Create(ProjectEmployee projectEmployee);
        //Task<ProjectEmployee> Edit(int id);
       // Task<ProjectEmployee> EditProjEmp(ProjectEmployee projectEmployee);
        Task<ProjectEmployee> Delete(int id);
        Task<ProjectEmployee> DeleteConfirmed(ProjectEmployee projectEmployee);
    }
}
