using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Domain.Models
{
    public class RoleClaim
    {
        [Key]
        public int Id { get; set; }

        public int ClaimId { get; set; }
        public Claim Claim { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
