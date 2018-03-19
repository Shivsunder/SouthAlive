using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SouthAlive.Data;
using Microsoft.AspNetCore.Identity;
using SouthAlive.Models;

namespace SouthAlive.Controllers
{
    public class MemberController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _um;

        public MemberController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> um
            )
        {
            _context = context;
            _um = um;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            
            return await _um.GetUserAsync(HttpContext.User);
        }

        public IActionResult Index()
        {
            var user = GetCurrentUser();
            return View(user);
        }
    }
}
