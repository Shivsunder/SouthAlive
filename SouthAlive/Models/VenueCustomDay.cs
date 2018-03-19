using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class VenueCustomDay
    {
        [Key]
        public int VenueCustomDayId { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public TimeSpan OpenTime { get; set; }

        [Required]
        public TimeSpan CloseTime { get; set; }

        public virtual int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
