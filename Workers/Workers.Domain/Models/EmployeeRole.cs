using System.ComponentModel.DataAnnotations;

namespace Workers.Domain.Models
{
    public class EmployeeRole
    {
        [Key]
        public int Id { get; set; }


        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
