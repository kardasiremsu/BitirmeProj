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
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BitirmeProj.Services;

namespace BitirmeProj.Controllers
{
    public class PostaJobController : Controller
    {
        // GET: Job/PostJob
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserSessionService _userSessionService;

        public PostaJobController(ApplicationDBContext context, IConfiguration configuration,IUserSessionService userSessionService)
        {
            _context = context;
            _configuration = configuration;
            _userSessionService = userSessionService;
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
            User currentUser = _userSessionService.GetCurrentUser();

            // Pass user data to the view
            ViewBag.CurrentUser = currentUser;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(JobListing model)
        {
            System.Diagnostics.Debug.WriteLine("HERE!!!!");
            try
            {
                JobListing job = new JobListing();
                job.JobTitle = model.JobTitle;
                job.JobDescription = model.JobDescription;
                job.JobLocation = model.JobLocation;
                job.JobType = model.JobType;
                job.ApplicationDeadline = model.ApplicationDeadline;
                job.WorkPlaceType = model.WorkPlaceType;
                job.ExperienceLevel = model.ExperienceLevel;
                job.Salary = model.Salary;
                job.SalaryCurrency = model.SalaryCurrency;
                User currentUser = _userSessionService.GetCurrentUser();
                
                job.PostedBy = currentUser.UserID;
                job.IsActive = 1;
                job.JobCreatedDate = DateTime.Now;
                _context.JobListings.Add(job);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Job posted successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("HERE error!");
                return Problem(ex.InnerException.Message);
            }

          
        }
    }
}

