﻿
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
    //[Route ("")]
    public class AdminController : Controller
    {
        private readonly IPartRepository _PartRepository;
        private User user = new User() { IsActive = true, Name = "Test", Username = "TestUserName", Admin = true };
        public AdminController(IPartRepository partRepository)
        {
            _PartRepository = partRepository;
        }
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Inventory()
        {
            return View();
        }
        [HttpPost]
        public async void AddItem(Part item)
        {
            var count = _PartRepository.GetAllParts(item.Name).Result;

            item.PartId = count.Count;
            await _PartRepository.AddPart(item);
            Index();
        }
        [HttpPost]
        public void RemoveItem(string name, int id)
        {
            Part a = _PartRepository.GetPart(name, id).Result;
            a.IsActive = false;
        }
        public IActionResult create()
        {
            return View();
        }
    }

}
