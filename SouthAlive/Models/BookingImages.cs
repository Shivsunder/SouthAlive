using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models
{
    public class BookingImages 
    {
        [Key]
        public int BookingImagesID { get; set; }

        public string ImagePath { get; set; }

        public virtual int VenueBookingID { get; set; }
        public virtual VenueBooking VenueBooking { get; set; }
    }
}
