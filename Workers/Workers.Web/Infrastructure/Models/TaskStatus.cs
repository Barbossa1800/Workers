using System;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class TaskStatus
    {
        [Key]
        public int Id { get; set; }
        public int StatusTaskId { get; set; }
        public Status StatusTask { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public DateTime DateEdit { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
