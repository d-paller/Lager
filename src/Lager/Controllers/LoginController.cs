using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Services.Repositories;
using Lager.Models;

namespace LagerCore.Controllers
{
    public class LoginController : Controller
    {
        // TODO: Add current user service to fetch and store the user in session

        [HttpGet]
        public IActionResult Login()
        {  
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            // If all fields contain data and are in the correct format
            if (ModelState.IsValid)
            {
                // If all the login info matches the database, if it doesn't send an error message
                if (user.IsValid(user.Username, user.Password))
                {
                    // if the user is an admin send them to the admin view, if not send them to the user view
                    if (user.IsAdmin())
                        RedirectToAction("Admin", "Index");
                    else
                        RedirectToAction("User", "Index");
                }
                else
                { 
                    ModelState.AddModelError("", "Invalid Login Credentials");
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            return Login();
        }
    }
}
