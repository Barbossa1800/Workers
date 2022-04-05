using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Domain.Models
{
    public class StatusProject
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Project> Projects { get; set; }
    }
}
