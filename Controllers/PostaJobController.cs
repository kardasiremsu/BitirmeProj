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

namespace BitirmeProj.Controllers
{
    public class PostaJobController : Controller
    {
        // GET: Job/PostJob
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;

        public PostaJobController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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


        //string jobTitle, string jobDescription, DateTime applicationDeadline, string jobLocation, int workPlaceType, int jobType, int experienceLevel, int salary, int salaryCurrency
        /*   joblisting job = new joblisting();
           job.jobıd = 1;
           job.jobtitle = jobtitle;
                   job.jobdescription = jobdescription;
                   job.joblocation = "test";
                   job.jobtype = jobtype;
                   job.applicationdeadline = applicationdeadline;
                   job.workplacetype = workplacetype;
                   job.experiencelevel = experiencelevel;
                   job.salary = salary;
                   job.salarycurrency = salarycurrency;
                   job.postedby = 1;
                   job.ısactive = 1;
                   job.jobcreateddate = datetime.now; 
                   _context.joblistings.add(job);
                   _context.savechanges();
                   return content("job listing created successfully");
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(JobListing model)
        {
            System.Diagnostics.Debug.WriteLine("!!!!!!");
            System.Diagnostics.Debug.WriteLine(model.WorkPlaceType);
            System.Diagnostics.Debug.WriteLine(model.JobTitle);
            System.Diagnostics.Debug.WriteLine(model.JobType);
            try
            {
                JobListing job = new JobListing();
                // job.JobID = 1;
                job.JobTitle = model.JobTitle;
                job.JobDescription = model.JobDescription;
                job.JobLocation = "test";
                job.JobType = model.JobType;
                job.ApplicationDeadline = model.ApplicationDeadline;
                job.WorkPlaceType = model.WorkPlaceType;
                job.ExperienceLevel = model.ExperienceLevel;
                job.Salary = model.Salary;
                job.SalaryCurrency = model.SalaryCurrency;
                //     job.PostedBy = GetCurrentUser().Id;
                job.PostedBy = 1;
                job.IsActive = 1;
                job.JobCreatedDate = DateTime.Now;
                _context.JobListings.Add(job);
                _context.SaveChanges();

                return Content("Job listing created successfully");
                //TempData["SuccessMessage"] = "Job listing created successfully";
                //return RedirectToAction("Index"); // Redirect to the desired action


            }
            catch (Exception ex)
            {

                return Problem(ex.InnerException.Message);
            }

           /* private ApplicationUser GetCurrentUser()
            {
                // Example implementation (replace with your actual authentication mechanism)
                return _context.Users.Find(1); // Assuming user ID 1 is the current user
            }
            /* try
             {
                 System.Diagnostics.Debug.WriteLine("try");
                 // Retrieve the connection string from appsettings.json
                 string connectionString = _configuration.GetConnectionString("DefaultConnection");

                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();
                     // SQL query to insert data into the database
                     string sql = $@"INSERT INTO joblistings (JobID, JobTitle, JobDescription, ApplicationDeadline, PostedBy, JobLocation, WorkPlaceType, JobType, ExperienceLevel, Salary, SalaryCurrency, isActive, JobCreatedDate)
                                     VALUES (@JobID, @JobTitle, @JobDescription, @ApplicationDeadline, @PostedBy, @JobLocation, @WorkPlaceType, @JobType, @ExperienceLevel, @Salary, @SalaryCurrency, @isActive, @JobCreatedDate)";

                     using (SqlCommand command = new SqlCommand(sql, connection))
                     {
                         // Use parameters to prevent SQL injection
                         command.Parameters.AddWithValue("@JobID", 3);

                         command.Parameters.AddWithValue("@JobTitle", jobTitle);
                         command.Parameters.AddWithValue("@JobDescription", jobDescription);
                         command.Parameters.AddWithValue("@ApplicationDeadline", applicationDeadline);
                         command.Parameters.AddWithValue("@PostedBy", 1);
                         command.Parameters.AddWithValue("@JobLocation", jobLocation);
                         command.Parameters.AddWithValue("@WorkPlaceType", workPlaceType);
                         command.Parameters.AddWithValue("@JobType", jobType);
                         command.Parameters.AddWithValue("@ExperienceLevel", experienceLevel);
                         command.Parameters.AddWithValue("@Salary", salary);
                         command.Parameters.AddWithValue("@SalaryCurrency", salaryCurrency);
                         command.Parameters.AddWithValue("@isActive", 1);
                         command.Parameters.AddWithValue("@jobCreatedDate", "10-10-2023");
                         command.ExecuteNonQuery();
                     }
                 }
                 return Content("Job listing created successfully");
             }
             catch (Exception ex)
             {
                 return Problem($"Error while posting the job: {ex.Message}");
             }*/
        }
    }
}

