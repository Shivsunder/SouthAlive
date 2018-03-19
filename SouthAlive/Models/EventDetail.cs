using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class EventDetail
    {
        [Key]
        public int EventDetailID { get; set; }

        public string VenueName { get; set; }
        public string VenueLocation { get; set; }

        public virtual int VenueBookingID { get; set; }
        public virtual VenueBooking VenueBooking { get; set; }
    }
}
