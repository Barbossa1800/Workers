using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Domain.Models
{
    public class EmployeeRegister
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указаа фамилия!")]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName => $"{LastName} {FirstName}";

        //public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль!")]
        public string PasswordHash { get; set; }

    //    public List<ProjectEmployee> ProjectEmployees { get; set; }
    //    public List<TaskStatus> InfoStatuses { get; set; }
    //    public List<EmployeeRole> EmployeeRoles { get; set; }
    }
}
