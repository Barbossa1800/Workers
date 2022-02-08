using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Constants;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;
using Extensions.Password;

namespace Workers.Web.Infrastructure
{
    public class DataSeeder
    {
        public static void SeedSystem(WorkerDbContext db)
        {
            if (!db.Roles.Any())
            {
                var role1 = new Role
                {
                    Name = CustomRole.Admin
                };
                var role2 = new Role
                {
                    Name = CustomRole.Manager
                };
                var role3 = new Role
                {
                    Name = CustomRole.Employee
                };

                db.Roles.AddRange(role1, role2, role3);
                db.SaveChanges();
            }


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
        }
    }
}
