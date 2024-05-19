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

        public HomeController(ApplicationDBContext context, IUserSessionService userSessionService)
        {
            _context = context;
            _userSessionService = userSessionService;
        }

        // JobDescription içinde skill geçiyorsa true, yoksa false döndüren fonksiyon
        private bool SkillExistsInJobDescription(string jobDescription, string skill)
        {
            return jobDescription.Contains(skill);
        }

        public async Task<IActionResult> Index(string sortOrder, string searchTerm, int? experienceLevel, string jobLocation, int? jobType, int page = 1, int pageSize = 10)
        {
            var jobs = from j in _context.JobListings select j;

            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "titleDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dataDesc" : "date";

            if (!string.IsNullOrEmpty(jobLocation))
            {
                jobs = jobs.Where(j => j.JobLocation == jobLocation);
            }
          
            if (!string.IsNullOrEmpty(searchTerm))
            {
                jobs = jobs.Where(j => j.JobTitle.Contains(searchTerm) || j.JobDescription.Contains(searchTerm));
            }

            if (experienceLevel != null)
            {
                jobs = jobs.Where(j => j.ExperienceLevel == experienceLevel);
            }

            if (jobType != null)
            {
                jobs = jobs.Where(j => j.JobType == jobType);
            }

            switch (sortOrder)
            {
                case "titleDesc":
                    jobs = jobs.OrderByDescending(j => j.JobTitle);
                    break;
                case "date":
                    jobs = jobs.OrderBy(j => j.JobCreatedDate);
                    break;
                case "dataDesc":
                    jobs = jobs.OrderByDescending(j => j.JobCreatedDate);
                    break;
                default:
                    jobs = jobs.OrderBy(j => j.JobTitle);
                    break;
            }

            User currentUser = _userSessionService.GetCurrentUser();
            // Get the current user's skills
         /*   var currentUserSkills = _context.UserSkills
                .Where(us => us.UserID == currentUser.UserID)
                .Select(us => us.Name) // Bu kısım değişti
                .ToList();

            System.Diagnostics.Debug.WriteLine("User Skill:");

            foreach (var skill in currentUserSkills)
            {
                System.Diagnostics.Debug.WriteLine("User Skill: " + skill);
            }

            // İş ilanlarını veritabanından çekmeden önce sadece becerilere göre filtreleme yapalım
            var skillRelatedJobs = jobs.ToList().Where(j => currentUserSkills.Any(skill => j.JobDescription.Contains(skill)));

            // Tüm iş ilanlarını çekelim
            var allJobs = await jobs.AsNoTracking().ToListAsync();

            // Diğer iş ilanlarından skillRelatedJobs içinde olmayanları alalım
            var otherJobs = allJobs.Except(skillRelatedJobs);

            // Skill related iş ilanlarını en üste getirelim ve geri kalanlarla birleştirelim
            var combinedJobs = skillRelatedJobs.Concat(otherJobs);

            // Geri kalan işlemleri uygulayalım (örneğin, sıralama)
            switch (sortOrder)
            {
                case "titleDesc":
                    combinedJobs = combinedJobs.OrderByDescending(j => j.JobTitle);
                    break;
                case "date":
                    combinedJobs = combinedJobs.OrderBy(j => j.JobCreatedDate);
                    break;
                case "dataDesc":
                    combinedJobs = combinedJobs.OrderByDescending(j => j.JobCreatedDate);
                    break;
                default:
                    combinedJobs = combinedJobs.OrderBy(j => j.JobTitle);
                    break;
            }

          */
        // Pass user data to the view
        ViewBag.CurrentUser = currentUser;
            var totalJobsCount = await jobs.CountAsync();
            ViewData["TotalJobsCount"] = totalJobsCount;

            //  return View(await jobs.AsNoTracking().ToListAsync());
            var paginatedJobs = await jobs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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
                application.CVID = 1;

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