using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class VenueBooking : IComparable<VenueBooking>
    {
        [Key]
        public int VenueBookingId { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public string EventName { get; set; }
        public string Description { get; set; }

        public bool IsPublic { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual int VenueId { get; set; }
        public virtual Venue Venue { get; set; }

        public virtual List<BookingImages> BookingImages { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual string UserId { get; set; }

        public int CompareTo(VenueBooking vb)
        {
            return (Year - vb.Year) * 365 + (Month - vb.Month) * 30 + (Day - vb.Day);
        }
    }
}
