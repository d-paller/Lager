
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

            var DbCount = _PartRepository.GetAllPart().Count();
            pagingInfo.PageCount = DbCount % pagingInfo.PageSize > 0 ?
                DbCount / pagingInfo.PageSize + 1 :
                DbCount / pagingInfo.PageSize;

            pagingInfo.CurrentPageIndex = 0;
            pagingInfo.SortField = "DateAdded";

            model.PagingInfo = pagingInfo;
            model.Parts = _PartRepository.GetAllPart().Take(pagingInfo.PageSize);

            return View(model);
        }


        [HttpPost]
        public IActionResult Inventory(InventoryViewModel model)
        {
            IQueryable<Part> query;
            if (model.search != null)
            {
                if (ModelState.IsValid)
                {
                    switch (model.search.field)
                    {
                        case "Category":
                            query = _PartRepository.GetAllPartsByCategory(model.search.value);
                            break;
                        case "Name":
                            query = _PartRepository.GetAllPartsByName(model.search.value);
                            break;
                        case "Holder":
                            query = _PartRepository.GetAllPartsByHolder(model.search.value);
                            break;
                        case "Vendor":
                            query = _PartRepository.GetAllPartsByVendor(model.search.value);
                            break;

                        default:
                            query = _PartRepository.GetAllPart();
                            break;
                    }
                }
                else
                    query = _PartRepository.GetAllPart();
            }
            else
                query = _PartRepository.GetAllPart();
            switch (model.PagingInfo.SortField)
            {
                case "Category":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Category) :
                        query.OrderBy(x => x.Category);
                    break;
                case "Name":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Name) :
                        query.OrderBy(x => x.Name);
                    break;
                case "PartId":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.PartId) :
                        query.OrderBy(x => x.PartId);
                    break;
                case "Cost":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Cost) :
                        query.OrderBy(x => x.Cost);
                    break;
                case "Holder":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Holder) :
                        query.OrderBy(x => x.Holder);
                    break;
                case "Vendor":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Vendor) :
                        query.OrderBy(x => x.Vendor);
                    break;
                case "PurchaseUrl":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.PurchaseUrl) :
                        query.OrderBy(x => x.PurchaseUrl);
                    break;

                default:
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.DateAdded) :
                        query.OrderBy(x => x.DateAdded);
                    break;
            }

            query = query.Skip(model.PagingInfo.CurrentPageIndex * model.PagingInfo.PageSize)
                .Take(model.PagingInfo.PageSize);

            model.Parts = query.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(PartViewModel item)
        {
            if (ModelState.IsValid)
            {
                var count = _PartRepository.GetAllParts(item.Part.Name).Result;
                if (item.Part.Holder == null)
                {
                    item.Part.Holder = "repo";
                }
                item.Part.PartId = count.Count + 1;
                item.Part.Category=item.Part.Category.ToLower();
                item.Part.Name = item.Part.Name.ToLower();
                item.Part.Vendor = item.Part.Vendor.ToLower();
                item.Part.Holder = item.Part.Holder.ToLower();
                await _PartRepository.AddPart(item.Part);
                return RedirectToAction("Inventory");
            }
            else
            {
                item.isValid = false;
                return View("Create");
            }
        }
        [HttpPost]
        public IActionResult findItem(string id)
        {
            Part a = _PartRepository.GetPart(id).Result;
            return View("Edit", a);

        }
        [HttpPost]
        public async Task<ActionResult> EditItem(PartViewModel a)
        {
            await _PartRepository.UpdatePart(a.Part.Id, a.Part);
            return RedirectToAction("Inventory");

        }
        [HttpPost]
        public async Task<ActionResult> RemoveItem(string id)
        {
            Part a = _PartRepository.GetPart(id).Result;
            a.IsActive = false;
            await _PartRepository.UpdatePart(a.Id, a);
            return RedirectToAction("Inventory");
        }
        public IActionResult create()
        {
            PartViewModel model = new PartViewModel();
            return View(model);
        }
        public string lame(string a)
        {
            return a;
        }

        public IActionResult edit()
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


