using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Constants;
using Extensions.Password;
using Workers.Infrastructure.Data.Context;
using Workers.Domain.Models;

namespace Workers.Web.Infrastructure
{
    public class DataSeeder
    {
        public static void SeedSystem(WorkerDbContext db)
        {
            #region Role
            if (!db.Roles.Any())
            {
                var roleAdmin = new Role
                {
                    Name = CustomRole.Admin
                };
                var roleManager = new Role
                {
                    Name = CustomRole.Manager
                };
                var roleEmployee = new Role
                {
                    Name = CustomRole.Employee
                };

                db.Roles.AddRange(roleAdmin, roleManager, roleEmployee);
                db.SaveChanges();
            }
            #endregion
            #region Employee
            if (!db.Employees.Any())
            {
                var newEmployee1 = new Employee {
                    FirstName = "Mykta",
                    LastName = "Medko",
                    Email = "nikita1800@gmail.com",
                    PasswordHash = "Nikita1234".GeneratePasswordHash()
                };

                db.Employees.Add(newEmployee1);
                db.SaveChanges();

                var employeeRole = new EmployeeRole
                {
                    EmployeeId = newEmployee1.Id,
                    RoleId = db.Roles.Where(x => x.Name == CustomRole.Admin).Select(s => new Role { Id = s.Id }).FirstOrDefault().Id,
                };
                db.EmployeeRoles.Add(employeeRole);
                db.SaveChanges();
            }
            #endregion
            #region Position
            if (!db.Positions.Any())
            {
                var newPositionDev = new Position
                {
                    Name = "Developer"
                };

                db.Positions.Add(newPositionDev);
                db.SaveChanges();
            }
            #endregion
            #region StatusProject
            if (!db.StatusProjects.Any())
            {
                var newStatusProject = new StatusProject
                {
                    Status = "New"
                };

                db.StatusProjects.Add(newStatusProject);
                db.SaveChanges();
            }
            #endregion
            #region Project
            if (!db.Projects.Any())
            {
                var now = DateTime.Now;
                var newProject = new Project
                {
                    Name = "Workers",
                    StartAt = DateTime.Now,
                    EndAt = new DateTime(now.Year, now.Month, now.Day + 2),
                    //StatusProject = db.StatusProjects.FirstOrDefault(x =>x.Status == );
                    StatusProjectId = db.StatusProjects.FirstOrDefault(x => x.Id == 1).Id
                };

                db.Projects.Add(newProject);
                db.SaveChanges();
            }
            #endregion
            #region ProjectEmployee
            if (!db.ProjectEmployees.Any())
            {
                var newProjectEmployee = new ProjectEmployee
                {
                    ProjectId = db.Projects.FirstOrDefault(x => x.Id == 1).Id,
                    EmployeeId = db.Employees.FirstOrDefault(x => x.Id == 1).Id,
                    PositionId = db.Positions.FirstOrDefault(x => x.Id == 1).Id
                };

                db.ProjectEmployees.Add(newProjectEmployee);
                db.SaveChanges();
            }
            #endregion
            #region Statuses
            if (!db.Statuses.Any())
            {
                var newStatuse = new Status
                {
                    StatusName = "Open"
                };

                db.Statuses.Add(newStatuse);
                db.SaveChanges();
            }
            #endregion
            #region Task
            if (!db.Tasks.Any())
            {
                var now = DateTime.Now;
                var newTask = new Domain.Models.Task
                {
                    DeadLine = new DateTime(now.Year, now.Month, now.Day + 2, now.Hour, now.Minute, now.Second),
                    Name = "Fixing bugs",
                    PercentOfCompleted = 82,
                    Description = "Something new happens every time",
                    ProjectId = db.Projects.FirstOrDefault(x => x.Id == 1).Id
                };

                db.Tasks.Add(newTask);
                db.SaveChanges();
            }
            #endregion
            #region TaskStatus
            if (!db.TaskStatuses.Any())
            {
                var now = DateTime.Now;
                var newTaskStatus = new Domain.Models.TaskStatus
                {
                    StatusTaskId = db.Statuses.FirstOrDefault(x => x.Id == 1).Id,
                    DateEdit = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second),
                    TaskId = db.Tasks.FirstOrDefault(x => x.Id == 1).Id,
                    EmployeeId = db.Employees.FirstOrDefault(x => x.Id == 1).Id
                };
                db.TaskStatuses.Add(newTaskStatus);
                db.SaveChanges();
            }
            #endregion
        }
    }
}
