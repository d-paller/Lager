using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Services.Repositories;
using Lager.Models;
using Lager.Services;
using Lager.Interfaces;

namespace LagerCore.Controllers
{
    public class LoginController : Controller
    {
        // TODO: Add current user service to fetch and store the user in session
        private UserLoginService _loginService;

        public LoginController(IUserRepository userRepo)
        {
            _loginService = new UserLoginService(userRepo);
        }

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
                    if (true) //(user.IsAdmin())
                        return RedirectToAction("Index", "Admin");
                    else
                       return  RedirectToAction("User", "Index");
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
