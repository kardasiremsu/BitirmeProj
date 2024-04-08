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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Printing; // Import the necessary namespaces
namespace BitirmeProj.Controllers

{
    public class PeopleController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserSessionService _userSessionService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IWebHostEnvironment _env;

        public PeopleController(ApplicationDBContext context, IUserSessionService userSessionService, IWebHostEnvironment webHostEnvironment, IWebHostEnvironment env)
        {
            _context = context;
            _userSessionService = userSessionService; // Inject the user session service
            _webHostEnvironment = webHostEnvironment;
            _env = env;
            var cvsDirectory = Path.Combine(env.WebRootPath, "cvs");
            if (!Directory.Exists(cvsDirectory))
            {
                Directory.CreateDirectory(cvsDirectory);
            }
        }

        // GET: People
        public IActionResult Index(int userId)
        {
            User currentUser = _userSessionService.GetCurrentUser();
            userId = (int)currentUser.UserID;

            // Retrieve the user information from the database based on the userId
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            var cvs = _context.CVs.Where(cv => cv.UserID == userId).ToList();

            // Populate the view model with the retrieved CVs
            
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
     
            var userSkillIDs = _context.UserSkills
        .Where(a => a.UserID == userId)
        .ToList();

          

            ViewBag.UserSkills = userSkillIDs;
            var WorkExperienceIDs = _context.UserWorks
      .Where(a => a.UserID == userId)
      .ToList();
            
            ViewBag.WorkExperiences = WorkExperienceIDs;
            var userLanguagesIDs = _context.UserLanguages
       .Where(a => a.UserID == userId)
       .ToList();

            ViewBag.UserLanguages = userLanguagesIDs;

            var userSchoolsID = _context.UserSchools
       .Where(a => a.UserID == userId)
       .ToList();



            ViewBag.UserSchools = userSchoolsID;
            

            // Pass job application IDs to the view using ViewBag
            var viewModel = new UserProfileViewModel { User = currentUser, JobApplicationIds = jobApplicationIds, CV = cvs};

            return View(viewModel);
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


        [HttpPost]
        public IActionResult DeleteCV(int CVID)
        {
            var cv = _context.CVs.Find(CVID);
            if (cv == null)
            {
                return NotFound();
            }

            _context.CVs.Remove(cv);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the index page after deletion
        }


        [HttpPost]
        public IActionResult UploadCV(IFormFile cvFile)
        {
            if (cvFile != null && cvFile.Length > 0)
            {
                try
                {
                    var currentUser = _userSessionService.GetCurrentUser(); // Assuming you have a service to get the current user
                    var user = _context.Users.Include(u => u.CVs).FirstOrDefault(u => u.UserID == currentUser.UserID);

                    // Create a new CV object
                    var newCV = new CV
                    {
                        UserID = currentUser.UserID,
                        CVName = cvFile.FileName // Set CVName to the original filename
                    };

                    // Save the CV object to the database
                    _context.CVs.Add(newCV);
                    _context.SaveChanges();
                    if (user != null)
                    {
                        user.CVs.Add(newCV); // Add the new CV to the user's CV collection
                        _context.SaveChanges(); // Save changes to the database
                    }
                    // Get the newly created CV ID
                    var cvId = newCV.CVID;

                    // Set the file path for saving
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "cvs", $"{cvId}_{cvFile.FileName}");


                    // Save the CV file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        cvFile.CopyTo(fileStream);
                    }
                    System.Diagnostics.Debug.WriteLine($" uploading CV is successfull");
                    // Redirect to the profile page or any other appropriate page
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file save failure)
                    System.Diagnostics.Debug.WriteLine($"Error uploading CV: {ex.Message}");
                    return RedirectToAction("Index"); // Redirect back to the profile page
                }
            }

            // Handle invalid file or other errors
            return RedirectToAction("Index"); // Redirect back to the profile page
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





        [HttpPost]
        public IActionResult AddSkill(UserSkill model)
        {
            System.Diagnostics.Debug.WriteLine("addskill");
            System.Diagnostics.Debug.WriteLine(model.Name);
            System.Diagnostics.Debug.WriteLine(model.Experience);
            User currentUser = _userSessionService.GetCurrentUser();
            var currentUserId = currentUser.UserID;
            System.Diagnostics.Debug.WriteLine("287");

            if (ModelState.IsValid)
            {
                // Check if the skill already exists in the database

                System.Diagnostics.Debug.WriteLine("291");

                var existingSkill = _context.Skills.FirstOrDefault(s => s.Name == model.Name);
                int skillId;
                var existingSkillForUser = _context.UserSkills.FirstOrDefault(s => s.Name == model.Name);

                // Eğer beceri mevcut değilse, yeni bir beceri oluştur ve veritabanına ekle
                if (existingSkill == null)
                {
                    System.Diagnostics.Debug.WriteLine("300");

                    Skill newSkill = new Skill { Name = model.Name };
                    _context.Skills.Add(newSkill);
                    _context.SaveChanges();

                    // Yeni becerinin SkillID'sini al
                    skillId = newSkill.SkillID;
                    UserSkill newUserSkill = new UserSkill
                    {

                        UserID = currentUserId,
                        SkillID = skillId,
                        Experience = model.Experience,
                        Name = model.Name
                    };

                    _context.UserSkills.Add(newUserSkill);
                    _context.SaveChanges();

                }
                else if(existingSkillForUser == null)
                {
                    System.Diagnostics.Debug.WriteLine("330");
                    skillId = existingSkill.SkillID;
                    // Kullanıcının beceri listesine yeni beceriyi ekle
                    UserSkill newUserSkill = new UserSkill
                    {

                        UserID = currentUserId,
                        SkillID = skillId,
                        Experience = model.Experience,
                        Name = model.Name
                    };

                    _context.UserSkills.Add(newUserSkill);
                    _context.SaveChanges();
                }
              



                return RedirectToAction("Index");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = error.ErrorMessage;
                // Hata mesajını işleyin veya hata detaylarını inceleyin
                System.Diagnostics.Debug.WriteLine(errorMessage);
            }
            System.Diagnostics.Debug.WriteLine("336");

            return View("Error");
        }
        [HttpPost]
        public IActionResult DeleteSkill(int SkillID)
        {
            // Gelen SkillID'ye sahip beceriyi bul
            var skillToDelete = _context.UserSkills.FirstOrDefault(s => s.SkillID == SkillID);

            if (skillToDelete != null)
            {
                try
                {
                    // Skill'i veritabanından sil
                    _context.UserSkills.Remove(skillToDelete);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Skill successfully deleted.";
                }
                catch (Exception ex)
                {
                    // Silme işlemi sırasında bir hata oluşursa, hata mesajını göster
                    TempData["ErrorMessage"] = $"An error occurred while deleting the skill: {ex.Message}";
                }
            }
            else
            {
                // Belirtilen SkillID'ye sahip bir beceri bulunamadıysa, hata mesajı göster
                TempData["ErrorMessage"] = "Skill not found.";
            }

            // Silme işleminden sonra kullanıcıyı bir yönlendirme yaparak başka bir sayfaya gönderin
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddWorkExperience(UserWork model)
        {
            if (ModelState.IsValid)
            {
                // Burada model verilerini işleyin ve veritabanına ekleyin
                // Örneğin:s
                var currentUser = _userSessionService.GetCurrentUser();
                System.Diagnostics.Debug.WriteLine(model.Title);
                System.Diagnostics.Debug.WriteLine(model.StartDate);
                var newWorkExperience = new UserWork
                {
                    UserID = currentUser.UserID,
            
                    StartDate = model.StartDate,
                    FinishDate = model.FinishDate,
                    Title = model.Title,
                    Institution = model.Institution,
                    Description = model.Description
                };

                _context.UserWorks.Add(newWorkExperience);
                _context.SaveChanges();

                // Eğer başarıyla eklendiyse, başka bir işlem yapabilir veya bir mesaj döndürebilirsiniz
                return RedirectToAction("Index", "Home"); // Örneğin, ana sayfaya yönlendirme
            }
            else
            {
                // Geçersiz model durumunda hata mesajları ile birlikte aynı formu tekrar görüntüleyin
                return View(model);
            }
        }

        public IActionResult DeleteWorkExperience(int UserWorkID)
        {
            // Gelen SkillID'ye sahip beceriyi bul
            var worktoDelete = _context.UserWorks.FirstOrDefault(s => s.UserWorksID == UserWorkID);

            if (worktoDelete != null)
            {
                try
                {
                    // Language'i veritabanından sil
                    _context.UserWorks.Remove(worktoDelete);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Work successfully deleted.";
                }
                catch (Exception ex)
                {
                    // Silme işlemi sırasında bir hata oluşursa, hata mesajını göster
                    TempData["ErrorMessage"] = $"An error occurred while deleting the works: {ex.Message}";
                }
            }
            else
            {
                // Belirtilen LanguageID'ye sahip bir beceri bulunamadıysa, hata mesajı göster
                TempData["ErrorMessage"] = "Work not found.";
            }

            // Silme işleminden sonra kullanıcıyı bir yönlendirme yaparak başka bir sayfaya gönderin
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AddLanguage(UserLanguage model)
        {
            System.Diagnostics.Debug.WriteLine("addLanguage");
            System.Diagnostics.Debug.WriteLine(model.Name);
            System.Diagnostics.Debug.WriteLine(model.Proficiency);
            User currentUser = _userSessionService.GetCurrentUser();
            var currentUserId = currentUser.UserID;

            if (ModelState.IsValid)
            {
                // Check if the Language already exists in the database


                var existingLanguage = _context.Languages.FirstOrDefault(s => s.Name == model.Name);
                int LanguageId;
                var existingLanguageForUser = _context.UserLanguages.FirstOrDefault(s => s.Name == model.Name);

                // Eğer beceri mevcut değilse, yeni bir beceri oluştur ve veritabanına ekle
                if (existingLanguage == null)
                {
                    
                    Language newLanguage = new Language { Name = model.Name };
                    _context.Languages.Add(newLanguage);
                    _context.SaveChanges();

                    // Yeni becerinin LanguageID'sini al
                    LanguageId = newLanguage.LanguageID;
                    UserLanguage newUserLanguage = new UserLanguage
                    {

                        UserID = currentUserId,
                        LanguageID = LanguageId,
                        Proficiency = model.Proficiency,
                        Name = model.Name
                    };

                    _context.UserLanguages.Add(newUserLanguage);
                    _context.SaveChanges();

                }
                else if (existingLanguageForUser == null)
                {
                    System.Diagnostics.Debug.WriteLine("330");
                    LanguageId = existingLanguage.LanguageID;
                    // Kullanıcının beceri listesine yeni beceriyi ekle
                    UserLanguage newUserLanguage = new UserLanguage
                    {

                        UserID = currentUserId,
                        LanguageID = LanguageId,
                        Proficiency = model.Proficiency,
                        Name = model.Name
                    };

                    _context.UserLanguages.Add(newUserLanguage);
                    _context.SaveChanges();
                }




                return RedirectToAction("Index");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = error.ErrorMessage;
                // Hata mesajını işleyin veya hata detaylarını inceleyin
                System.Diagnostics.Debug.WriteLine(errorMessage);
            }
            System.Diagnostics.Debug.WriteLine("336");

            return View("Error");
        }
        [HttpPost]
        public IActionResult DeleteLanguage(int LanguageID)
        {
            // Gelen SkillID'ye sahip beceriyi bul
            var languagetoDelete = _context.UserLanguages.FirstOrDefault(s => s.LanguageID == LanguageID);

            if (languagetoDelete != null)
            {
                try
                {
                    // Language'i veritabanından sil
                    _context.UserLanguages.Remove(languagetoDelete);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Language successfully deleted.";
                }
                catch (Exception ex)
                {
                    // Silme işlemi sırasında bir hata oluşursa, hata mesajını göster
                    TempData["ErrorMessage"] = $"An error occurred while deleting the Language: {ex.Message}";
                }
            }
            else
            {
                // Belirtilen LanguageID'ye sahip bir beceri bulunamadıysa, hata mesajı göster
                TempData["ErrorMessage"] = "Language not found.";
            }

            // Silme işleminden sonra kullanıcıyı bir yönlendirme yaparak başka bir sayfaya gönderin
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AddSchool(UserSchool model)
        {
            if (ModelState.IsValid)
            {
                // Burada model verilerini işleyin ve veritabanına ekleyin
                // Örneğin:s
                var currentUser = _userSessionService.GetCurrentUser();
               
                var newSchoolExperience = new UserSchool
                {
                    UserID = currentUser.UserID,

                    GraduationYear = model.GraduationYear,
                    Name = model.Name,
                    Degree = model.Degree
                };

                _context.UserSchools.Add(newSchoolExperience);
                _context.SaveChanges();

                // Eğer başarıyla eklendiyse, başka bir işlem yapabilir veya bir mesaj döndürebilirsiniz
                return RedirectToAction("Index", "Home"); // Örneğin, ana sayfaya yönlendirme
            }
            else
            {
                // Geçersiz model durumunda hata mesajları ile birlikte aynı formu tekrar görüntüleyin
                return View(model);
            }
        }


        public IActionResult DeleteSchool(int UserSchoolID)
        {
            // Gelen SkillID'ye sahip beceriyi bul
            var schooltoDelete = _context.UserSchools.FirstOrDefault(s => s.UserSchoolID == UserSchoolID);

            if (schooltoDelete != null)
            {
                try
                {
                    // Language'i veritabanından sil
                    _context.UserSchools.Remove(schooltoDelete);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "School successfully deleted.";
                }
                catch (Exception ex)
                {
                    // Silme işlemi sırasında bir hata oluşursa, hata mesajını göster
                    TempData["ErrorMessage"] = $"An error occurred while deleting the School: {ex.Message}";
                }
            }
            else
            {
                // Belirtilen LanguageID'ye sahip bir beceri bulunamadıysa, hata mesajı göster
                TempData["ErrorMessage"] = "School not found.";
            }

            // Silme işleminden sonra kullanıcıyı bir yönlendirme yaparak başka bir sayfaya gönderin
            return RedirectToAction("Index");
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
