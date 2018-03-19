using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SouthAlive.Data;
using SouthAlive.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SouthAlive.Controllers
{
    public class VenueController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> Book(int id)
        {
            ViewBag.id = id;
            ViewBag.month = DateTime.Today.Month;
            ViewBag.year = DateTime.Today.Year;
            Venue venue = _context.Venue.SingleOrDefault(m => m.VenueId == id);
            ViewBag.Venue = venue;
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            ViewBag.VenueBooking = _context.VenueBooking.ToList();
            return View(await _context.VenueBooking.ToListAsync());
        }
        public async Task<IActionResult> Approve()
        {
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            ViewBag.VenueBooking = _context.VenueBooking.ToList();
            return View(await _context.Venue.ToListAsync());
        }
        public async Task<IActionResult> ViewBookings(int id)
        {
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            if (id == 0) { ViewBag.VenueBooking = _context.VenueBooking.ToList(); }
            else
            {
                ViewBag.forstuff = _context.VenueBooking.ToList();
                Venue venue = _context.Venue.SingleOrDefault(m => m.VenueId == id);
                if (venue.VenueBookings != null) { ViewBag.VenueBooking = venue.VenueBookings.ToList(); }
                else { ViewBag.VenueBooking = new List<Venue>(); }
            }
            return View(await _context.Venue.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.VenueImages = _context.VenueImages.ToList();
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            return View(await _context.Venue.ToListAsync());
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions;
            ViewBag.VenueCustomDay = _context.VenueCustomDay;
            return View(await _context.Venue.ToListAsync());
        }

        [HttpPost, ActionName("AddVenue")]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> AddVenue(string name, string description, string address, int timetype, int price, List<TimeSpan> opening, List<TimeSpan> closing, List<int> day)
        {
            var currentuser = await _usermanager.GetUserAsync(User);
            Venue venue = new Venue();
            venue.VenueName = name;
            venue.Price = price;
            venue.Description = description;
            venue.Address = address;
            venue.VenueTimesOptionsId = timetype;
            _context.Add(venue);
            _context.SaveChanges();

            int id = venue.VenueId;
            for (int i = 0; i < day.Count; i++)
            {
                Console.Write(opening.Count);
                VenueCustomDay cday = new VenueCustomDay();
                cday.VenueId = id;
                cday.Day = day[i];
                cday.OpenTime = opening[i];
                cday.CloseTime = closing[i];
                _context.Add(cday);
                _context.SaveChanges();
            }
            return new JsonResult(id);
        }
        public ActionResult GetForm(int day, int id, int month, int year)
        {
            ViewBag.day = day;
            ViewBag.id = id;
            ViewBag.month = month;
            ViewBag.year = year;
            Venue send = _context.Venue.SingleOrDefault(m => m.VenueId == id);
            ViewBag.Venue = send;
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            ViewBag.VenueBooking = _context.VenueBooking.ToList();
            return PartialView("_bookingForm");
        }

        public ActionResult NextMonth(int id, int month, int year)
        {
            ViewBag.id = id;
            ViewBag.month = month;
            ViewBag.year = year;
            ViewBag.Venue = _context.Venue.SingleOrDefault(m => m.VenueId == id);
            ViewBag.VenueTimesOptions = _context.VenueTimesOptions.ToList();
            ViewBag.VenueCustomDay = _context.VenueCustomDay.ToList();
            ViewBag.VenueBooking = _context.VenueBooking.ToList();
            return PartialView("_Calendar");
        }

        public async Task<IActionResult> ConfirmBooking(int id)
        {
            VenueBooking book = _context.VenueBooking.Find(id);
            book.IsApproved = true;
            if (await TryUpdateModelAsync<VenueBooking>(book, "", s => s.IsApproved))
            {
                _context.SaveChanges();
            }
            _context.Update(book);
            return Redirect("/Venue");
        }

        
        public async Task<IActionResult> Remove(int id)
        {
            Venue venue = _context.Venue.Find(id);
            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();
            return (Redirect("/Venue"));
        }

        public async Task<IActionResult> AddImages(int id, IFormFile[] Images)
        {
            
            if (!(Images == null || Images.Length == 0))
            {
                foreach (IFormFile image in Images)
                {
                    string b = "" + new Random().Next() + image.FileName.Substring(image.FileName.IndexOf('.'));
                    List<VenueImages> list = _context.VenueImages.ToList();
                    while (list.Where(m => m.ImagePath == "/VenueImages/" + b).Count() > 0)
                    {
                        b = "" + new Random().Next() + image.FileName.Substring(image.FileName.IndexOf('.'));
                    }
                    string filename = b;
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/VenueImages",
                                filename);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    VenueImages vi = new VenueImages();
                    vi.ImagePath = "/VenueImages/" + filename;
                    vi.VenueID = id;
                    _context.Add(vi);
                    _context.SaveChanges();
                }
            }
            return (Redirect("/Venue/Edit"));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ID = id;
            return View(await _context.Venue.SingleOrDefaultAsync(m => m.VenueId==id));
        }

        public async Task<IActionResult> RequestBooking(int id, int day, int month, int year, TimeSpan startTime, TimeSpan endTime, string EventName,string description, string IsPublic, IFormFile[] Images, string venueName, string location)
        {
            VenueBooking book = new VenueBooking();
            book.IsApproved = false;
            book.VenueId = id;
            book.Day = day;
            book.Month = month;
            book.Year = year;
            book.StartTime = startTime;
            book.EndTime = endTime;
            book.EventName = EventName;
            book.Description = description;
            if (IsPublic == null) { book.IsPublic = false; }
            else { book.IsPublic = true; }
            if (User.IsInRole("Admin"))
            {
                book.IsApproved = true;
                if (venueName != null)
                {
                    book.IsFeatured = true;
                    book.IsPublic = true;
                }
            }
            book.UserId = _usermanager.GetUserId(User);
            _context.Add(book);
            await _context.SaveChangesAsync();

            if (User.IsInRole("Admin"))
            {
                if (venueName != null)
                {
                    EventDetail EV = new EventDetail();
                    EV.VenueBookingID = book.VenueBookingId;
                    EV.VenueLocation = location;
                    EV.VenueName = venueName;
                    _context.Add(EV);
                    await _context.SaveChangesAsync();
                }
            }


            if (!(Images == null || Images.Length == 0))
            {
                foreach (IFormFile image in Images)
                {
                    string b = ""+new Random().Next()+ image.FileName.Substring(image.FileName.IndexOf('.'));
                    List<BookingImages> list = _context.BookingImages.ToList();
                    while(list.Where(m => m.ImagePath == "/BookingImages/" + b).Count() > 0)
                    {
                        b = "" + new Random().Next() + image.FileName.Substring(image.FileName.IndexOf('.'));
                    }
                    string filename = b;
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/BookingImages",
                                filename);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    BookingImages bi = new BookingImages();
                    bi.ImagePath = "/BookingImages/" + filename;
                    bi.VenueBookingID = book.VenueBookingId;
                    _context.Add(bi);
                    _context.SaveChanges();
                }
            }

            return Redirect("/Venue");
        }
    }
}