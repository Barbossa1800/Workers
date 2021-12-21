using System;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class InfoStatus
    {
        [Key]
        public int Id { get; set; }
        public int StatusTaskId { get; set; }
        public StatusTask StatusTask { get; set; }
        public DateTime DateEdit { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
