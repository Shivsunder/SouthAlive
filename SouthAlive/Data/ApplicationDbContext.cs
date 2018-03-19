using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SouthAlive.Models;
using SouthAlive.Models.SampleEditor;

namespace SouthAlive.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<SouthAlive.Models.Polyline> Polyline { get; set; }

        public DbSet<SouthAlive.Models.LineInfo> LineInfo { get; set; }

        public DbSet<SouthAlive.Models.coordinateConstraint> coordinateConstraint { get; set; }

        public DbSet<SouthAlive.Models.Team> Team { get; set; }

        public DbSet<SouthAlive.Models.UserTeam> UserTeam { get; set; }

        public DbSet<SouthAlive.Models.Venue> Venue { get; set; }

        public DbSet<SouthAlive.Models.VenueBooking> VenueBooking { get; set; }

        public DbSet<SouthAlive.Models.VenueCustomDay> VenueCustomDay { get; set; }

        public DbSet<SouthAlive.Models.VenueTimesOptions> VenueTimesOptions { get; set; }

        public DbSet<SouthAlive.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<SouthAlive.Models.SampleEditor.Editor> Editor { get; set; }

        public DbSet<SouthAlive.Models.PageIntroduction> PageIntroduction { get; set; }

        public DbSet<SouthAlive.Models.NewsLetterPDF> NewsLetterPDF { get; set; }

        public DbSet<SouthAlive.Models.BookingImages> BookingImages { get; set; }

        public DbSet<SouthAlive.Models.VenueImages> VenueImages { get; set; }

        public DbSet<SouthAlive.Models.EventDetail> EventDetail { get; set; }
    }
}
