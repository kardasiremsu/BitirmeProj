using BitirmeProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
using Microsoft.Data.Sqlite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BitirmeProj.Controllers
{
    public class HomeController : Controller
    {
    //    private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult PostJob()
        {

            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostJob([Bind("ID,PersonID,Title,JobType,JobLocation,Description,Skills,Questions,Active,Date")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new SelectList(_context.Persons, "ID", "ID", job.PersonID);
            return View(job);
        }

        // GET: Jobs
        public async Task<IActionResult> Index(string sortOrder, string titleString, string locationString)
        {
            var jobs = from j in _context.Jobs select j;

            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "titleDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dataDesc" : "date";

            ViewData["TitleFilter"] = titleString;

            if (!String.IsNullOrEmpty(titleString))
            {
                jobs = jobs.Where(j => j.Title.Contains(titleString) || j.Description.Contains(titleString));
            }

            ViewData["LocationFilter"] = locationString;

            if (!String.IsNullOrEmpty(locationString))
            {
                jobs = jobs.Where(j => j.JobLocation.Contains(locationString));
            }


            switch (sortOrder)
            {
                case "titleDesc":
                    jobs = jobs.OrderByDescending(j => j.Title);
                    break;
                case "date":
                    jobs = jobs.OrderBy(j => j.Date);
                    break;
                case "dataDesc":
                    jobs = jobs.OrderByDescending(j => j.Date);
                    break;
                default:
                    jobs = jobs.OrderBy(j => j.Title);
                    break;
            }


            return View(await jobs.AsNoTracking().ToListAsync());
           
            }


        
       
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MyJobs()
        {
            return View();

        }
        public IActionResult MyApplications()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}