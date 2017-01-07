
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Models;

namespace Lager.Controllers
{
    public class AdminController : Controller
    {
        private User user = new User() { IsActive = true, Name = "Test", Username = "TestUserName", Admin = true };

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void AddItem()
        {

        }
    }

}
