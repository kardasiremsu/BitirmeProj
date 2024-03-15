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
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ApplicationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Applications.Include(a => a.CV).Include(a => a.JobListing).Include(a => a.User);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.CV)
                .Include(a => a.JobListing)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["CVID"] = new SelectList(_context.CVs, "CVID", "CVID");
            ViewData["JobID"] = new SelectList(_context.JobListings, "JobID", "JobID");
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationID,UserID,JobID,ApplicationDate,Status,ApplicationNote,CoverLetter,CVID")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CVID"] = new SelectList(_context.CVs, "CVID", "CVID", application.CVID);
            ViewData["JobID"] = new SelectList(_context.JobListings, "JobID", "JobID", application.JobID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", application.UserID);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["CVID"] = new SelectList(_context.CVs, "CVID", "CVID", application.CVID);
            ViewData["JobID"] = new SelectList(_context.JobListings, "JobID", "JobID", application.JobID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", application.UserID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,UserID,JobID,ApplicationDate,Status,ApplicationNote,CoverLetter,CVID")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
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
            ViewData["CVID"] = new SelectList(_context.CVs, "CVID", "CVID", application.CVID);
            ViewData["JobID"] = new SelectList(_context.JobListings, "JobID", "JobID", application.JobID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", application.UserID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.CV)
                .Include(a => a.JobListing)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Applications'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
          return (_context.Applications?.Any(e => e.ApplicationID == id)).GetValueOrDefault();
        }
    }
}
