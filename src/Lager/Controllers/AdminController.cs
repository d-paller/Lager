
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Models;
using Lager.Services.Repositories;
using Lager.Interfaces;
using Microsoft.Extensions.Options;

namespace Lager.Controllers
{
    public class AdminController : Controller
    {
        private UserRepository _userRepo;
        // DataAccess partList = new DataAccess();
        //IList<DataAccess> partInventory partList.GetParts();
        //ViewData.Model = new ViewResult
        //{ partInventory = partList; };
        //return View(Part);

        //private readonly IPartRepository partRepo;

        public AdminController()
        {
        }

        private User user = new User() { IsActive = true, Name = "Test", Username = "TestUserName", Admin = true };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPart()
        {
            //how to post?
            //_PartRepository.AddPart(item);
            return View();
        }

        public async Task<IActionResult> Users()
        {
            IList<User> users = new List<User>() {
                new Models.User()
                {
                    Username = "test1",
                    Name = "Billy Joe"
            },
                new User()
                {
                    Username = "test2",
                    Name = "Dave"
                }
            }; //await _userRepo.GetAll();
            return View(users);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            return View();
        }

        public IActionResult Backup()
        {
            return View();
        }

    }
}


