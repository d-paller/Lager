
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Models;
using Lager.Services.Repositories;
using Lager.Interfaces;

namespace Lager.Controllers
{
    public class AdminController : Controller
    {

        // DataAccess partList = new DataAccess();
        //IList<DataAccess> partInventory partList.GetParts();
        //ViewData.Model = new ViewResult
        //{ partInventory = partList; };
        //return View(Part);

        //private readonly IPartRepository partRepo;

        private User user = new User() { IsActive = true, Name = "Test", Username = "TestUserName", Admin = true };

        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Inventory()
        {
            return View();
        }

        public IActionResult Create()
        {
            //how to post?
            //_PartRepository.AddPart(item);
            return View();
        }

        }
    }

}
