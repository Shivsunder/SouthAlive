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


namespace SouthAlive.Controllers
{
    public class ZeroRubbishController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ApplicationDbContext _context;

        public ZeroRubbishController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.cc = _context.coordinateConstraint.ToList();
            ViewBag.LineInfo = _context.LineInfo.ToList();
            return View(await _context.Polyline.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ZeroRubbishList()
        {
            ViewBag.LineInfo = _context.LineInfo.ToList();
            return View(await _context.Polyline.ToListAsync());
        }

        public async Task<IActionResult> Area()
        {
            return View(await _context.coordinateConstraint.ToListAsync());
        }

        [HttpPost, ActionName("Add")]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Add(List<String> lines, string name, string phone)
        {
            var currentuser = await _usermanager.GetUserAsync(User);

            foreach (string n in lines)
            {
                Polyline line = new Polyline();
                line.PolyLine = n;
                line.DateAdded = DateTime.Now;
                line.LastChecked = DateTime.Now;
                line.UserId = currentuser.Id;
                _context.Add(line);
                await _context.SaveChangesAsync();
                if (User.IsInRole("ZeroRubbish"))
                {
                    LineInfo li = new LineInfo();
                    li.PolyLineId = line.PolylineId;
                    li.Name = name;
                    li.PhoneNumber = phone;
                    _context.Add(li);
                    await _context.SaveChangesAsync();
                }
            }
            return new JsonResult("done");
        }

        [HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var currentuser = await _usermanager.GetUserAsync(User);
            Polyline poly = _context.Polyline.Find(id);
            if (User.IsInRole("Admin") || currentuser.Id == poly.UserId)
            {
                _context.Polyline.Remove(poly);
                _context.SaveChanges();
            }
            return (Redirect("/ZeroRubbish"));
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int id)
        {
            Polyline poly = _context.Polyline.Find(id);
            _context.Polyline.Remove(poly);
            _context.SaveChanges();
            return (Redirect("/ZeroRubbish/ZeroRubbishList"));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Check(int id)
        {
            Polyline poly = _context.Polyline.Find(id);
            poly.LastChecked = DateTime.Now;
            _context.Polyline.Update(poly);
            _context.SaveChanges();
            return (Redirect("/ZeroRubbish/ZeroRubbishList"));
        }
    }
}