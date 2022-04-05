using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Domain.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }
    }
}
