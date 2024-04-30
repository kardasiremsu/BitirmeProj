using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using BitirmeProj.Data;
using BitirmeProj.Models;
using BitirmeProj.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using NuGet.Common;

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
        private readonly SignInManager<RegisterViewModel> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<RegisterViewModel> _userManager;

        public AccountController(ApplicationDBContext context, IUserSessionService userSessionService, UserManager<RegisterViewModel> userManager, SignInManager<RegisterViewModel> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userSessionService = userSessionService; // Inject the user session service
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
            var random = new Random();
            var token2 = random.Next(1, 999999);
            if (ModelState.IsValid)
            {
                var user = new RegisterViewModel
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    UserType = model.UserType,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Title = model.Title,
                    Phone = model.Phone,
                    Address = model.Address,
                    Institution = model.Institution,
                    Password = model.Password,
                    EmailConfirmed = false, // Mark email as not confirmed initially
                    Token = token2
                };

                //try
                //{
                    User new_user = new User();
                    new_user.UserName = model.UserName;
                    new_user.Email = model.Email;
                    var passwordHasher = new PasswordHasher<User>();
                    string hashedPassword = passwordHasher.HashPassword(null, model.Password);
                    new_user.Password = hashedPassword;
                    new_user.UserType = model.UserType;
                    new_user.FirstName = model.FirstName;
                    new_user.LastName = model.LastName;
                    new_user.Gender = model.Gender;
                    new_user.Title = model.Title;
                    new_user.Phone = model.Phone;
                    new_user.Address = model.Address;
                    new_user.Institution = model.Institution;
                    new_user.Token = token2;
                    _context.Users.Add(new_user);
                    _context.SaveChanges();
                   // TempData["SuccessMessage"] = "User is created successfully";

                 //   return RedirectToAction("Login");



               // }
                /*catch (Exception ex)
                {

                    return Problem(ex.InnerException.Message);
                }*/
                /*
                // Display success message and redirect to login page
                TempData["SuccessMessage"] = "User registered successfully. Please check your email for verification.";
                return RedirectToAction("Login");*/

              


                var mailMessage = new MailMessage();
                mailMessage.To.Add(model.Email);
                mailMessage.From = new MailAddress("thesisproject2604@hotmail.com");

                mailMessage.Subject = "Test Subject";
                // Token ile birlikte aktivasyon bağlantısı gönder
                mailMessage.Body = $"Please click the following link to activate your account: <a href='{Url.Action("Activation", "Account", new { userId = new_user.UserID.ToString(), token = new_user.Token.ToString() }, Request.Scheme)}'>Activate Account</a> Your Token: {new_user.Token}"; mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
               // smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = new NetworkCredential("thesisproject2604@hotmail.com", "thesisproject1!");
                smtpClient.Port = 587; // Gmail SMTP port
                smtpClient.Host = "smtp-mail.outlook.com";
                smtpClient.EnableSsl = true;     // Enable SSL/TLS
                System.Diagnostics.Debug.WriteLine("153");


                try
                {
                    smtpClient.Send(mailMessage);
                    TempData["Message"] = "Mail is sent succesfully";
                    System.Diagnostics.Debug.WriteLine("mail");


                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("mail girmeid");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    TempData["Message"] = "Mail could not send. Error:" + ex.Message;
                }

                /*
                var result = await _userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine("succedded");

                    // Generate email confirmation token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                    // Send email verification email
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your email address", $"Please confirm your account by <a href=''>clicking here</a>.");

              
               }
                else
                {
                    System.Diagnostics.Debug.WriteLine("failed");

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        System.Diagnostics.Debug.WriteLine($"Error: {error.Code}, Description: {error.Description}");

                    }
                }*/
            }

            // If we reach here, something went wrong, redisplay form with errors
            return View(model);
        }

        [HttpGet]
        public IActionResult ActivationSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Activation(string userId, string token)
        {
            ActivationViewModel activation = new ActivationViewModel();
            activation.UserId = userId;
            activation.Token = token;
            return View(activation);
        }


        [HttpGet]
        public async Task<IActionResult> ActivateAccount(string userId, string token)
        {
            // Kullanıcıyı userId'e göre bulun
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);

            if (user == null)
            {
                // Geçersiz userId'i işleyin (kullanıcı bulunamadı)
                return RedirectToAction("ActivationFailed");
            }

            // Kullanıcının tokenini doğrulayın
            if (user.Token != null && user.Token.Equals(token))
            {
                // Token doğrulandı, hesabı etkinleştirin veya başka işlemler yapın
                user.isActive = true; // Örneğin, hesabı etkinleştirin
                await _context.SaveChangesAsync();

                // Görünümü döndürürken UserId ve Token bilgilerini iletmek için ViewModel kullanın
                var viewModel = new ActivationViewModel { UserId = userId, Token = token };
                return View(viewModel);
            }
            else
            {
                // Token doğrulama başarısız oldu
                return RedirectToAction("ActivationFailed");
            }
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                // Email confirmed successfully
                return View("ConfirmEmail");
            }
            else
            {
                // Email confirmation failed
                return RedirectToAction("Error", "Home");
            }
        }

        // Other methods
    }

}
