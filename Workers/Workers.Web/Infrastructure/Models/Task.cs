using System;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public string Name { get; set; }
        public double PercentOfCompleted { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int InfoStatusId { get; set; }
        public InfoStatus InfoStatus { get; set; }
    }
}
