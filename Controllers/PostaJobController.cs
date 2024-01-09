using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
using BitirmeProj.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitirmeProj.Controllers
{
    public class PostaJobController : Controller
    {
        // GET: Job/PostJob
        private readonly ApplicationDBContext _context;

        public PostaJobController(ApplicationDBContext context)
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

        public IActionResult PostJob()
        {
            return View();
        }

  
       
        [HttpPost]
        [ValidateAntiForgeryToken] // Add this attribute for security
        public async Task<IActionResult> PostJob([Bind("JobID,JobTitle,JobDescription,ApplicationDeadline,PostedBy,JobLocation,WorkPlaceType,JobType,ExperienceLevel,Salary,SalaryCurrency,IsActive,JobCreatedDate")] JobListing job)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Set additional properties if needed before saving
                    job.JobCreatedDate = DateTime.Now; // For example, setting the creation date
                    // Assuming PostedBy is the current user's ID, replace this with your actual logic to get the current user's ID
                    job.PostedBy = 1; 
                    job.JobID = 3;
                    
                    _context.JobListings.Add(job);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(JobPostedSuccessfully));
                }
                catch (Exception ex)
                {
               
                    return Problem($"Error while posting the job: {ex.Message}");
                }
            }

            // If ModelState is not valid, return to the same view with validation errors
            return View(job);
        }

        // GET: Job/JobPostedSuccessfully
        public ActionResult JobPostedSuccessfully()
        {
            return View();
        }
    }
}
