using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
using BitirmeProj.Models;
using BitirmeProj.Services;
namespace BitirmeProj.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserSessionService _userSessionService;
        public PeopleController(ApplicationDBContext context, IUserSessionService userSessionService)
        {
            _context = context;
            _userSessionService = userSessionService; // Inject the user session service
        }

        // GET: People
        public IActionResult Index(int userId)
        {
            User currentUser = _userSessionService.GetCurrentUser();
            userId = (int)currentUser.UserID;

            // Retrieve the user information from the database based on the userId
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Retrieve job application IDs for the user using SQL query
            var jobApplicationIds = _context.Applications
                .Where(a => a.UserID == userId)
                .Select(a => a.ApplicationID)
                .ToList();
            var userJobs = _context.JobListings
        .Where(j => j.PostedBy == userId)
        .ToList();
           
            // Pass job application IDs and job listings to the view using ViewBag
            ViewBag.JobApplicationIds = jobApplicationIds;
         

            // Pass job application IDs and user jobs to the view using ViewBag
            ViewBag.UserJobs = userJobs;
            // Pass job application IDs to the view using ViewBag


            return View(user);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            User currentUser = _userSessionService.GetCurrentUser();
            id = currentUser.UserID;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var User = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Password,FirstName,LastName,Email,Phone")] User User)
        {
            if (ModelState.IsValid)
            {
                _context.Add(User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(User);
        }

        // Inside PeopleController.cs

        // GET: People/Edit
        public IActionResult Edit()
        {
            User currentUser = _userSessionService.GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserID == currentUser.UserID);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: People/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,FirstName,LastName,Email,Phone")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
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
            return View(user);
        }

        /*  // GET: People/Edit/5
          public async Task<IActionResult> Edit(int? id)
          {
              if (id == null || _context.Users == null)
              {
                  return NotFound();
              }

              var User = await _context.Users.FindAsync(id);
              if (User == null)
              {
                  return NotFound();
              }
              return View(User);
          }

          // POST: People/Edit/5
          // To protect from overposting attacks, enable the specific properties you want to bind to.
          // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(int id, [Bind("ID,Password,FirstName,LastName,Email,Phone")] User User)
          {
              if (id != User.UserID)
              {
                  return NotFound();
              }

              if (ModelState.IsValid)
              {
                  try
                  {
                      _context.Update(User);
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      if (!UserExists(User.UserID))
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
              return View(User);
          }
        */

        public IActionResult Edit(int ?userId)
        {

            User currentUser = _userSessionService.GetCurrentUser();
            userId = currentUser.UserID;
            // Retrieve the user information from the database based on the userId
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                // Handle the case where the user is not found
                return NotFound();
            }

            // Pass the user object to the edit view
            return View(user);
        }
        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var User = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Users'  is null.");
            }
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                _context.Users.Remove(User);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
