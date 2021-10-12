using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperNite.Data;
using SuperNite.Models;

namespace SuperNite.Controllers
{
    public class LinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Links
        public async Task<IActionResult> Index()
        {
            return View(await _context.Links.ToListAsync());
        }

        // GET: Links/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Links/ShowSearchResults

        public async Task<IActionResult> ShowSearchResults(String SearchApp)
        {
            return View("Index", await _context.Links.Where(j => j.App.Contains(SearchApp)).ToListAsync());
        }


        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var links = await _context.Links
                .FirstOrDefaultAsync(m => m.id == id);
            if (links == null)
            {
                return NotFound();
            }

            return View(links);
        }

        // GET: Links/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,App,Link")] Links links)
        {
            if (ModelState.IsValid)
            {
                _context.Add(links);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(links);
        }

        // GET: Links/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var links = await _context.Links.FindAsync(id);
            if (links == null)
            {
                return NotFound();
            }
            return View(links);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,App,Link")] Links links)
        {
            if (id != links.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(links);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinksExists(links.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(links);
        }

        // GET: Links/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var links = await _context.Links
                .FirstOrDefaultAsync(m => m.id == id);
            if (links == null)
            {
                return NotFound();
            }

            return View(links);
        }

        // POST: Links/Delete/5

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var links = await _context.Links.FindAsync(id);
            _context.Links.Remove(links);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinksExists(int id)
        {
            return _context.Links.Any(e => e.id == id);
        }
    }
}
