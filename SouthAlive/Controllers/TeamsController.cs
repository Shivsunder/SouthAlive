using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SouthAlive.Data;
using SouthAlive.Models;
using Microsoft.AspNetCore.Identity;

namespace SouthAlive.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;


        public TeamsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
             )
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }


        public IActionResult Team1()
        {
            return View("Team1/Team1");
        }

        public IActionResult Team2()
        {
            return View("Team2/Team2");
        }

        public IActionResult Team3()
        {
            return View("Team3/Team3");
        }

        public IActionResult Team4()
        {
            return View("Team4/Team4");
        }

        public IActionResult Team5()
        {
            return View("Team5/Team5");
        }

        public IActionResult Team6()
        {
            return View("Team6/Team6");
        }

        public IActionResult Team7()
        {
            return View("Team7/Team7");
        }

        public IActionResult Team8()
        {
            return View("Team8/Team8");
        }

        public IActionResult Team9()
        {
            return View("Team9/Team9");
        }

        [Authorize(Roles = "Team1Admin")]
        public async Task<IActionResult> Team1List()
        {
            return View("Team1/Team1List", await _context.ApplicationUser.ToListAsync());
        }

        [Authorize(Roles = "Team1Admin")]
        public async Task<IActionResult> Team1Manage()
        {
            return View("Team1/Team1Manage", await _context.ApplicationUser.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam1(string id, [Bind("RoleName")] string RoleName)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _userManager.Users.FirstOrDefaultAsync(r => r.Id == id);
                var result = await _userManager.AddToRoleAsync(CurrentUser, RoleName);

                return RedirectToAction("Team1Manage");
            };

            return View("Team1/Team1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTeam1(string id, [Bind("RoleName")] string RoleName)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _userManager.Users.FirstOrDefaultAsync(r => r.Id == id);
                var result = await _userManager.RemoveFromRoleAsync(CurrentUser, RoleName);

                return RedirectToAction("Team1Manage");
            };

            return View("Team1/Team1");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, [Bind("RoleName")] string RoleName)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _userManager.Users.FirstOrDefaultAsync(r => r.Id == id);
                var result = await _userManager.AddToRoleAsync(CurrentUser, RoleName);

                return RedirectToAction("Index");
            };

            return View("Teams");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string id, [Bind("RoleName")] string RoleName)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _userManager.Users.FirstOrDefaultAsync(r => r.Id == id);
                var result = await _userManager.RemoveFromRoleAsync(CurrentUser, RoleName);

                return RedirectToAction("Index");
            };

            return View("Teams");
        }

        [Authorize(Roles = "Admin")]
        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [Authorize(Roles = "Admin")]
        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamId == id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}
