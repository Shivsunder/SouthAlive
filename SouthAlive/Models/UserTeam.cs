using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class UserTeam
    {
        [Key]
        public int UserTeamId { get; set; }

        [Required]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
