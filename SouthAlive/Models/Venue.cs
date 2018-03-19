using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        public string VenueName { get; set; }

        [Required]
        public string Description { get; set; }

        public int Price { get; set; }

        public string Address { get; set; }

        public bool IsSecret { get; set; }

        public virtual List<VenueImages> VenueImages { get; set; }

        public virtual int VenueTimesOptionsId { get; set; }
        public virtual VenueTimesOptions VenueTimesOptions { get; set; }

        public virtual List<VenueCustomDay> VenueCustomDays { get; set; }

        public virtual List<VenueBooking> VenueBookings { get; set; }
    }
}
