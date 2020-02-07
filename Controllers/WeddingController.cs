using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using 

namespace LoginRegistration.Controllers
{
    [Route("wedding")]
    public class WeddingController : Controller
    {
        private WeddingController dbContext;
        public WeddingController(HomeContext context)
        {
            dbContext = context;
        }
// ///////////////////////////////////////////////////////////////////////
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            User userInDb = LoggedIn(); // taken from the home controller method
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            // does the many to many relationship and the one to many relationship at the same time
            List<Wedding> AllWeddings = dbContext.Weddings
                .Include( w => w.GuestList )
                .ThenInclude( r => r.Guest )
                .Include ( w => w.Planner )
                .ToList();

            ViewBag.User = userInDb;
            return View();

        }
// //////////////////////////////////////////////////////////////////////
        [HttpGet("new/wedding")]
        public IActionResult NewWedding()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            ViewBag.User = userInDb;
            return View();
        }
// ///////////////////////////////////////////////////////////////////
        [HttpPost("create/wedding")]
        public IActionResult CreateWedding()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            if(ModelState.IsValid)
            {
                dbContext.Weddings.Add.(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.User = userInDb;
                return View("NewWedding");
            }
        }
// ////////////////////////////////////////////////////////////
        [HttpGet("{weddingId}")]
        public IActionResult ShowWedding()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            Wedding show = dbContext.Weddings
                .Include( w => w.GuestList )
                .ThenInclude( r => r.Guest )
                .Include( w => w.WeddingId)
            return View();
        }
// ///////////////////////////////////////////////////////
        [HttpGet("delete/{weddingId}")]
        public IActionResult DeleteWedding(int weddingId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            Wedding remove = dbContext.Weddings.FirstOrDefault( w => w.WeddingId == weddingId);
            dbContext.Weddings.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("")

        }
// ////////////////////////////////////////////////
        [HttpGet("response/{weddingId}/{userId}/{status}")]
        public IActionResult Rsvp(int weddingId)
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            Wedding show = dbContext.Weddings
                .Include( w => w.GuestList )
                .ThenInclude( r => r.Guest )
                .Include( w => w.WeddingId);
            return View();
        }


    }
}