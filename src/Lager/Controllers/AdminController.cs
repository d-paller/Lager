
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lager.Models;
using Lager.Services.Repositories;
using Lager.Interfaces;
using Microsoft.Extensions.Options;
using Lager.Services;
using MongoDB.Driver.Linq;
using Lager.Models.ViewModels;

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
            InventoryViewModel model = new InventoryViewModel();
            PagingInfo pagingInfo = new PagingInfo();

            pagingInfo.SortDesc = true;
            pagingInfo.PageSize = 20;
            pagingInfo.PageCount = Convert.ToInt32(Math.Ceiling((double)(_PartRepository.GetAllPart().Count() / pagingInfo.PageSize)));
            pagingInfo.CurrentPageIndex = 0;
            pagingInfo.SortField = "DateAdded";

            model.PagingInfo = pagingInfo;
            model.Parts = _PartRepository.GetAllPart();

            return View();
        }


        [HttpPost]
        public IActionResult Inventory(PagingInfo pagingInfo)
        {
            IQueryable<Part> query = _PartRepository.GetAllPart();

            pagingInfo.SortDesc = true;
            pagingInfo.PageSize = 20;
            pagingInfo.PageCount = Convert.ToInt32(Math.Ceiling((double)(_PartRepository.GetAllPart().Count() / pagingInfo.PageSize)));
            pagingInfo.CurrentPageIndex = 0;

            switch (pagingInfo.SortField)
            {
                case "Category":
                    query = pagingInfo.SortDesc?
                        query.OrderByDescending(x => x.Category) :
                        query.OrderBy(x => x.Category);
                    break;
                case "Name":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Name) :
                        query.OrderBy(x => x.Name);
                    break;
                case "PartId":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.PartId) :
                        query.OrderBy(x => x.PartId);
                    break;
                case "Cost":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Cost) :
                        query.OrderBy(x => x.Cost);
                    break;
                case "Holder":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Holder) :
                        query.OrderBy(x => x.Holder);
                    break;
                case "Vendor":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Vendor) :
                        query.OrderBy(x => x.Vendor);
                    break;
                case "PurchaseUrl":
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.PurchaseUrl) :
                        query.OrderBy(x => x.PurchaseUrl);
                    break;

                default:
                    query = pagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.DateAdded) :
                        query.OrderBy(x => x.DateAdded);
                    break;
            }
            return View(query.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(PartViewModel item)
        {
            if (ModelState.IsValid) { 
            var count = _PartRepository.GetAllParts(item.Part.Name).Result;
                if(item.Part.Holder == null){
                    item.Part.Holder = "Repo";
                }
            item.Part.PartId = count.Count + 1;
            await _PartRepository.AddPart(item.Part);
            return Inventory(true);
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

        public IActionResult Test()
        {
            User newUser = new User();
            return View(newUser);
        }

        [HttpPost]
        public IActionResult Test(User user)
        {
            SCryptPasswordHasher hasher = new SCryptPasswordHasher();

            user.Password = hasher.HashPassword(user, user.Password);
            return View(user);
        }


    }
}


