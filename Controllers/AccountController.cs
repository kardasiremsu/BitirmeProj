using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProj.Data;
using BitirmeProj.Models;

namespace BitirmeProj.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDBContext _context;

        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }


        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                       View(await _context.Users.ToListAsync()) :
                       Problem("Entity set 'ApplicationDBContext.Jobs'  is null.");
        }


      

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
               
                bool isAuthenticated = true;

                if (isAuthenticated)
                {
                    // Redirect authenticated user to a dashboard or desired page
                    return RedirectToAction("Index", "Home"); // Change "Index" and "Home" to your desired page
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If login fails, return to the login page with an error message
            return View(model);
        }
    }
}
