using BitirmeProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
///using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProj.Services;
using Microsoft.AspNetCore.Identity; // Import the necessary namespaces

namespace BitirmeProj.Controllers
{
    public class HomeController : Controller
    {
        //    private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDBContext _context;
        private readonly IUserSessionService _userSessionService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDBContext context, IUserSessionService userSessionService, ILogger<HomeController> logger)
        {
            _context = context;
            _userSessionService = userSessionService;
            _logger = logger;

        }
        [HttpPost]
        public IActionResult LogLoadTime([FromBody] LoadTimeLog log)
        {
            if (log == null)
            {
                _logger.LogError("Log is null");
                return BadRequest("Log is null");
            }

            // Log the load time (e.g., save to database or file)
            _logger.LogInformation($"Path: {log.Path}, Load Time: {log.LoadTime} ms");

            return Ok();
        }
        // JobDescription içinde skill geçiyorsa true, yoksa false döndüren fonksiyon
        private bool SkillExistsInJobDescription(string jobDescription, string skill)
        {
            return jobDescription.Contains(skill);
        }

        public async Task<IActionResult> Index(string sortOrder, string searchTerm, int? experienceLevel, string jobLocation, int? jobType, int page = 1, int pageSize = 10)
        {
            var jobsQuery = from j in _context.JobListings select j;

            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "titleDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dataDesc" : "date";

            if (!string.IsNullOrEmpty(jobLocation))
            {
                jobsQuery = jobsQuery.Where(j => j.JobLocation == jobLocation);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                jobsQuery = jobsQuery.Where(j => j.JobTitle.Contains(searchTerm) || j.JobDescription.Contains(searchTerm));
            }

            if (experienceLevel != null)
            {
                jobsQuery = jobsQuery.Where(j => j.ExperienceLevel == experienceLevel);
            }

            if (jobType != null)
            {
                jobsQuery = jobsQuery.Where(j => j.JobType == jobType);
            }

            User currentUser = _userSessionService.GetCurrentUser();

            var currentUserSkills = _context.UserSkills
                                             .Where(us => us.UserID == currentUser.UserID)
                                             .Select(us => us.Name)
                                             .ToList();

            System.Diagnostics.Debug.WriteLine("User Skills:");
            foreach (var skill in currentUserSkills)
            {
                System.Diagnostics.Debug.WriteLine("User Skill: " + skill);
            }

            List<JobListing> skillRelatedJobs = new List<JobListing>();
            List<JobListing> combinedJobs;

            if (currentUserSkills.Any())
            {
                // If the user has skills, filter and prioritize skill-related jobs
                var allJobs = await jobsQuery.AsNoTracking().ToListAsync();
                skillRelatedJobs = allJobs.Where(j => currentUserSkills.Any(skill => j.JobDescription.Contains(skill))).ToList();

                System.Diagnostics.Debug.WriteLine("Skill Related Jobs:");
                foreach (var job in skillRelatedJobs)
                {
                    var matchingSkills = currentUserSkills.Where(skill => job.JobDescription.Contains(skill)).ToList();
                    System.Diagnostics.Debug.WriteLine("Job Title: " + job.JobTitle + " - Matching Skills: " + string.Join(", ", matchingSkills));
                }

                // Get jobs that are not skill-related
                var otherJobs = allJobs.Except(skillRelatedJobs).ToList();

                // Combine skill-related jobs with other jobs
                combinedJobs = skillRelatedJobs.Concat(otherJobs).ToList();
            }
            else
            {
                // If the user has no skills, get all jobs
                combinedJobs = await jobsQuery.AsNoTracking().ToListAsync();
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "titleDesc":
                    combinedJobs = combinedJobs.OrderByDescending(j => j.JobTitle).ToList();
                    break;
                case "date":
                    combinedJobs = combinedJobs.OrderBy(j => j.JobCreatedDate).ToList();
                    break;
                case "dataDesc":
                    combinedJobs = combinedJobs.OrderByDescending(j => j.JobCreatedDate).ToList();
                    break;
                default:
                    combinedJobs = combinedJobs.ToList();
                    break;
            }

            ViewBag.CurrentUser = currentUser;

            var appliedJobIds = await _context.Applications
                                              .Where(a => a.UserID == currentUser.UserID)
                                              .Select(a => a.JobID)
                                              .ToListAsync();

            ViewBag.AppliedJobIds = appliedJobIds;

            var totalJobsCount = combinedJobs.Count();
            ViewData["TotalJobsCount"] = totalJobsCount;

            var paginatedJobs = combinedJobs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.SkillRelatedJobs = skillRelatedJobs; // Pass the skill-related jobs to the view

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_JobListing", paginatedJobs);
            }

            return View(paginatedJobs);
        }




        public IActionResult CreateApplication()
        {
            return View();
        }

    
      

        [HttpPost]
        public IActionResult CreateApplication(int jobID)
        {
            try
            {
                User currentUser = _userSessionService.GetCurrentUser();
                // Assuming you have the necessary logic to create the application record
                Application application = new Application();

                application.JobID = jobID;
                application.UserID = currentUser.UserID;
                application.ApplicationDate = DateTime.Now;
                application.Status = "Pending"; // Set the initial status as per your requirement
                application.ApplicationNote = "applicationNote";
                application.CoverLetter = "coverLetter";
                application.CVID = 23;

                _context.Applications.Add(application);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Application posted successfully";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Return an error response
                return StatusCode(500, $"An error occurred while creating the application: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult LoadJobs(int page = 1, int pageSize = 10)
        {
            var jobs = _context.JobListings
                               .OrderBy(j => j.ApplicationDeadline)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            return PartialView("_JobList", jobs);
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


        [HttpGet]
        public IActionResult SkillRelatedJobs()
        {
            // Get the current user (you need to implement your own logic to get the current user)
            User currentUser = _userSessionService.GetCurrentUser();
            if (currentUser != null)
            {
                // Get user's skills
                var userSkills = _context.UserSkills
               .Where(us => us.UserID == currentUser.UserID)
               .Join(_context.Skills,
                   us => us.SkillID,
                   s => s.SkillID,
                   (us, s) => s.Name)
               .ToList();
                // Get job listings where the description contains at least one of the user's skills
                var skillRelatedJobs = _context.JobListings
                    .Where(j => userSkills.Any(skill => j.JobDescription.Contains(skill)))
                    .ToList();

                // Redirect to the home page with the filtered job listings
                return RedirectToAction("Index", "Home", new { skillRelatedJobs });
            }

            // If user is not logged in or has no skills, redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public class LoadTimeLog
{
    public string Path { get; set; }
    public string LoadTime { get; set; }
}