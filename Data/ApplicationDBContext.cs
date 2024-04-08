using BitirmeProj.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Application = BitirmeProj.Models.Application;


namespace BitirmeProj.Data
{
    public class ApplicationDBContext :DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<UserSchool> UserSchools { get; set; }
        public DbSet<UserWork> UserWorks { get; set; }
        public DbSet<UserReference> UserReferences { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<JobListingSkill> JobListingSkills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<JobPreference> JobPreferences { get; set; }

    }
 
}


