using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SouthAlive.Data;
using SouthAlive.Models;

namespace SouthAlive.Controllers
{
    public class PageIntroductionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageIntroductionController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PageIntroduction
        public async Task<IActionResult> Index()
        {
            return View(await _context.PageIntroduction.ToListAsync());
        }

        // GET: PageIntroduction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageIntroduction = await _context.PageIntroduction
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pageIntroduction == null)
            {
                return NotFound();
            }

            return View(pageIntroduction);
        }

        // GET: PageIntroduction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageIntroduction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text")] PageIntroduction pageIntroduction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageIntroduction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pageIntroduction);
        }

        // GET: PageIntroduction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageIntroduction = await _context.PageIntroduction.SingleOrDefaultAsync(m => m.Id == id);
            if (pageIntroduction == null)
            {
                return NotFound();
            }
            return View(pageIntroduction);
        }

        // POST: PageIntroduction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text")] PageIntroduction pageIntroduction)
        {
            if (id != pageIntroduction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageIntroduction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageIntroductionExists(pageIntroduction.Id))
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
            return View(pageIntroduction);
        }

        // GET: PageIntroduction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageIntroduction = await _context.PageIntroduction
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pageIntroduction == null)
            {
                return NotFound();
            }

            return View(pageIntroduction);
        }

        // POST: PageIntroduction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageIntroduction = await _context.PageIntroduction.SingleOrDefaultAsync(m => m.Id == id);
            _context.PageIntroduction.Remove(pageIntroduction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PageIntroductionExists(int id)
        {
            return _context.PageIntroduction.Any(e => e.Id == id);
        }
    }
}
