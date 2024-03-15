using System.Linq;
using BitirmeProj.Data;
using BitirmeProj.Models;
using BitirmeProj.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProj.Controllers
{
    public class AccountController : Controller
    {
        private bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
        private readonly ApplicationDBContext _context;
        private readonly IUserSessionService _userSessionService;
        public AccountController(ApplicationDBContext context, IUserSessionService userSessionService)
        {
            _context = context;
            _userSessionService = userSessionService; // Inject the user session service
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                // Kullanıcı varsa ve parola doğrulanıyorsa
                if (user != null && VerifyPassword(user.Password, model.Password))
                {
                    _userSessionService.SetCurrentUser(user);
                    User currentUser = _userSessionService.GetCurrentUser();
                    System.Diagnostics.Debug.WriteLine("Current Uuser!!");
                    System.Diagnostics.Debug.WriteLine(currentUser.UserID);
                    // Log in successful, redirect to dashboard or home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           

            try
            {
                User user = new User();
                user.UserName = model.UserName;
                user.Email = model.Email;
                var passwordHasher = new PasswordHasher<User>();
                string hashedPassword = passwordHasher.HashPassword(null, model.Password);
                user.Password = hashedPassword;
                user.UserType = model.UserType;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.Title = model.Title;
                user.Phone = model.Phone;
                user.Address = model.Address;
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "User is created successfully";

                return RedirectToAction("Login");



            }
            catch (Exception ex)
            {

                return Problem(ex.InnerException.Message);
            }

        }

    }
}