using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
using BitirmeProj.Models;

namespace BitirmeProj.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public JobsController(ApplicationDBContext context)
        {
            _context = context;
        }

       
        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return _context.JobListings != null ?
                       View(await _context.JobListings.ToListAsync()) :
                       Problem("Entity set 'ApplicationDBContext.Jobs'  is null.");
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var job = await _context.JobListings
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(m => m.JobID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PersonID,Title,JobType,JobLocation,Description,Skills,Questions,Active,Date")] JobListing job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new SelectList(_context.Users, "ID", "ID", job.PostedBy);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var job = await _context.JobListings.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(_context.Users, "ID", "ID", job.PostedBy);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PersonID,Title,JobType,JobLocation,Description,Skills,Questions,Active,Date")] JobListing job)
        {
            if (id != job.JobID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobID))
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
           // ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "ID", job.PersonID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var job = await _context.JobListings
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(m => m.JobID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobListings == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Jobs'  is null.");
            }
            var job = await _context.JobListings.FindAsync(id);
            if (job != null)
            {
                _context.JobListings.Remove(job);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
          return (_context.JobListings?.Any(e => e.JobID == id)).GetValueOrDefault();
        }
    }
}
