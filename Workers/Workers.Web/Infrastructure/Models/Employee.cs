using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workers.Web.Infrastructure.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName => $"{LastName} {FirstName}";
        //public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public List<ProjectEmployee> ProjectEmployees { get; set; }
        public List<TaskStatus> InfoStatuses { get; set; }
        public List<EmployeeRole> EmployeeRoles { get; set; }
    }
}
