using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private HomeContext dbContext;
        public HomeController(HomeContext context)
        {
            dbContext = context;
        }
// the login page - where app starts
        public IActionResult Index()
        {
            return View();
        }
// registering new members
        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View();
        }
// the main site after a successful sign in
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            User userInDb = LoggedIn();
            Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$${userInDb.UserId}");
            if(userInDb == null)
            {
                return RedirectToAction("LogOut");
            }
            else
            {
            ViewBag.User = userInDb;
            return View("Dashboard");
            }
        }

        [HttpPost("register")]
        public IActionResult Register(User register)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any( u => u.Email == register.Email))
                {
                    ModelState.AddModelError("Email", "A User with that email already exists.");
                    return View("Registration");
                }
                else
                {
                    PasswordHasher<User> hash = new PasswordHasher<User>();
                    register.Password = hash.HashPassword(register, register.Password);

                    dbContext.Users.Add(register);
                    dbContext.SaveChanges();
    // will have to use session here to redirect to dashboard, otherwise redirect to login page
                    return RedirectToAction("Index");
                        // HttpContext.Session.SetInt32("UserId", confirmUser.UserId);
                        // return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return View("Registration");
            }
        }

// user sign in, with validation checks
        [HttpPost("signin")]
        public IActionResult SignIn(LoginUser log)
        {
            if(ModelState.IsValid)
            {
                User confirmUser = dbContext.Users.FirstOrDefault( u => u.Email == log.LoginEmail);
                if(confirmUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Email and Password do not match");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<LoginUser> compare = new PasswordHasher<LoginUser>();
                    var result = compare.VerifyHashedPassword(log, confirmUser.Password, log.LoginPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LoginEmail", "Email and Password do not match");
                        return View("Index");
                    }
                    else
                    {
                        Console.WriteLine("*****THIS SHOULD TAKE ME TO THE DASHBOARD PAGE*******************************************");
                        HttpContext.Session.SetInt32("UserId", confirmUser.UserId);
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private User LoggedIn()
        {
            return dbContext.Users.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }



////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
