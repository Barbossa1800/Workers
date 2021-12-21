using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workers.Web.Infrastructure.Models
{
    public class StatusTask
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public List<InfoStatus> InfoStatuses { get; set; }
    }
}
