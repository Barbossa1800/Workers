using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
