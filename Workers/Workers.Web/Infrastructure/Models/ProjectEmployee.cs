using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class ProjectEmployee
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
