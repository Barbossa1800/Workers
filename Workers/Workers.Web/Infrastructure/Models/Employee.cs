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
        public string Login { get; set; }
        public string Email { get; set; }
        public List<ProjectEmployee> ProjectEmployees { get; set; }
        public List<InfoStatus> InfoStatuses { get; set; }
    }
}
