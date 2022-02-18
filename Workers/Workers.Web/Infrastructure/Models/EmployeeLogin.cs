using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class EmployeeLogin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль!")]
        public string PasswordHash { get; set; }

        //public List<ProjectEmployee> ProjectEmployees { get; set; }
        //public List<TaskStatus> InfoStatuses { get; set; }
        //public List<EmployeeRole> EmployeeRoles { get; set; }
    }
}
