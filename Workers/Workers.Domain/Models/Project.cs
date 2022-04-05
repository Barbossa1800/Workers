using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Domain.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public List<ProjectEmployee> ProjectEmployees { get; set; }
        public List<Task> Tasks { get; set; }

        public int StatusProjectId { get; set; }
        public StatusProject StatusProject { get; set; }
    }
}
