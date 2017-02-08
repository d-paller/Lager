
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
            var a =  _PartRepository.GetAllPart().Result;
            return View(a);
        }
        [HttpPost]
        public async Task<ActionResult> AddItem(PartViewModel item)
        {
            if (ModelState.IsValid) { 
            var count = _PartRepository.GetAllParts(item.Part.Name).Result;
                if(item.Part.Holder == null){
                    item.Part.Holder = "Repo";
                }
            item.Part.PartId = count.Count + 1;
            await _PartRepository.AddPart(item.Part);
            return View("Index");
            }
            else
            {
                item.isValid = false;
                return View("create");
            }
        }
        [HttpPost]
        public async Task<ActionResult> RemoveItem(string name, int id)
        {
            Part a = _PartRepository.GetPart(name, id).Result;
            a.IsActive = false;
            await _PartRepository.UpdatePart(a.Id, a);
            return View();
        }
        public IActionResult create()
        {
            PartViewModel model = new PartViewModel();
            return View(model);
        }
    }
}


