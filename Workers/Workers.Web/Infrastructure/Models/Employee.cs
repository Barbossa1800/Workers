using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public List<ProjectEmployee> ProjectEmployees { get; set; }
        public List<InfoStatus> InfoStatuses { get; set; }
    }
}
