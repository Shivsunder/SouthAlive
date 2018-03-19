using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SouthAlive.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<Polyline> Polylines { get; set; }
        public virtual List<UserTeam> UserTeams { get; set; }
        public virtual List<VenueBooking> VenueBookings { get; set; }
    }
}
