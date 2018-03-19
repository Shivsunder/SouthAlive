using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SouthAlive.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using SouthAlive.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthAlive.Controllers
{
    public class AboutController : Controller
    {
        private ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Introduction()
        {
            
            return View(_context.PageIntroduction.ToList());
        }

        public IActionResult Organisation()
        {
            return View();
        }

        public IActionResult Teams_And_Projects()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View();
        }

        public IActionResult SponsorInformation()
        {
            return View();
        }
        public IActionResult LocalInformation()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SampleEditor()
        {
            return View();
        }

        public IActionResult Events()
        {
            ViewBag.Images = _context.BookingImages.ToList();
            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.eventinfo = _context.EventDetail.ToList();
            return View(_context.VenueBooking.ToList());
        }

        public IActionResult Newsletter()
        {
            return View(_context.NewsLetterPDF.ToList());
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Newsletter(string title, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return View();
            }
            string filename = file.FileName;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/PDFfiles",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            NewsLetterPDF pdf = new NewsLetterPDF();
            pdf.Path = "/PDFfiles/"+file.FileName;
            pdf.Title = title;
            _context.Add(pdf);
            _context.SaveChanges();
            return View(_context.NewsLetterPDF.ToList());
        }
    }
}
