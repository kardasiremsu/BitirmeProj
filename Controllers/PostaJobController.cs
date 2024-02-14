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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(string jobTitle, string jobDescription, DateTime applicationDeadline, string jobLocation, int workPlaceType, int jobType, int experienceLevel, decimal salary, int salaryCurrency)
        {
            try
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
            }
        }
    }
}

