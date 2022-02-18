using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<EmployeeRole> EmployeeRoles { get; set; }
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
