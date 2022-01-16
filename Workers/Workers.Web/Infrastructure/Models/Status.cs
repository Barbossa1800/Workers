using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
        public List<TaskStatus> InfoStatuses { get; set; }
    }
}
