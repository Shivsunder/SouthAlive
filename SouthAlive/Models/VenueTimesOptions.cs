using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class VenueTimesOptions
    {
        [Key]
        public int VenueTimesOptionsId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Venue> Venues { get; set; }
    }
}
