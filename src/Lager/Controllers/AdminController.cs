
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
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        [HttpGet]
        public async Task<IActionResult> Inventory()
        {

            InventoryViewModel model = new InventoryViewModel();
            PagingInfo pagingInfo = new PagingInfo();

            pagingInfo.SortDesc = true;
            pagingInfo.PageSize = 10;
            pagingInfo.CurrentPageIndex = 0;
            pagingInfo.SortField = "Category";

            var Db = await _PartRepository.GetAllPart();
            var DbList = Db.ToList();
            var DbCount = DbList.Count();

            pagingInfo.PageCount = DbCount % pagingInfo.PageSize > 0 ?
                DbCount / pagingInfo.PageSize + 1 :
                DbCount / pagingInfo.PageSize;

            model.PagingInfo = pagingInfo;
            model.Parts = DbList.Take(pagingInfo.PageSize);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Inventory(InventoryViewModel model)
        {
            IQueryable<Part> query;

            query = await _PartRepository.GetAllPart();
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
                case "Vendor":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Vendor) :
                        query.OrderBy(x => x.Vendor);
                    break;

                case "DateCheckedOut":
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.DateCheckedOut) :
                        query.OrderBy(x => x.DateCheckedOut);
                    break;
                default:
                    query = model.PagingInfo.SortDesc ?
                        query.OrderByDescending(x => x.Category) :
                        query.OrderBy(x => x.Category);
                    break;
            }

            query = query.Skip(model.PagingInfo.CurrentPageIndex * model.PagingInfo.PageSize)
                .Take(model.PagingInfo.PageSize);

            model.Parts = query.ToList();
            return View(model);
        }


        public IActionResult InventorySearch(InventoryViewModel model)
        {
            IQueryable<Part> query = model.Parts.AsQueryable();

            if (model.Parts.Count() != 0)
            {
                var pagingInfo = new PagingInfo();
                pagingInfo.SortDesc = true;
                pagingInfo.PageSize = 10;
                pagingInfo.CurrentPageIndex = 0;
                pagingInfo.SortField = "Category";

                model.PagingInfo = pagingInfo;

                var count = model.Parts.Count();
                model.PagingInfo.PageCount = count % model.PagingInfo.PageSize > 0 ?
                        count / model.PagingInfo.PageSize + 1 :
                        count / model.PagingInfo.PageSize;

                query = query.Skip(model.PagingInfo.CurrentPageIndex * model.PagingInfo.PageSize)
                    .Take(model.PagingInfo.PageSize);

                model.Parts = query.ToList();
            }
            return View("Inventory", model);
        }


        [HttpPost]
        public async Task<IActionResult> duplicate(string id)
        {
            Part a = _PartRepository.GetPart(id).Result;
            Part b = new Part();
            b.Holder = "lab";
            var count = await _PartRepository.GetAllParts(a.Name);
            b.PartId = count.Count + 1 ;
            b.Name = a.Name;
            b.PurchaseUrl = a.PurchaseUrl;
            b.Category = a.Category;
            b.Cost = a.Cost;
            b.Description = a.Description;
            b.DatePurchased = DateTime.Now;
            b.Vendor = a.Vendor;
            b.VendorID = a.VendorID;
            await _PartRepository.AddPart(b);
            return await Inventory();

        }
        [HttpPost]
        public async Task<IActionResult> AddItem(PartViewModel item)
        {
            if (ModelState.IsValid)
            {
                var count = await _PartRepository.GetAllParts(item.Part.Name);

                item.Part.PartId = count.Count + 1;
                item.Part.Category = item.Part.Category.ToLower();
                item.Part.Name = item.Part.Name.ToLower();
                item.Part.Vendor = item.Part.Vendor.ToLower();
                if(item.Part.Holder ==null){
                    item.Part.Holder = "lab";
                }
                else
                {
                    item.Part.Holder = item.Part.Holder.ToLower();
                }

                await _PartRepository.AddPart(item.Part);
                return RedirectToAction("Inventory");
            }
            else
            {
                item.isValid = false;
                return await Inventory();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditItem(PartViewModel p)
        {
            await _PartRepository.UpdatePart(p.Part.Id, p.Part);
            return RedirectToAction("Inventory");

        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(string id)
        {
            Part a = _PartRepository.GetPart(id).Result;
            await _PartRepository.UpdatePart(a.Id, a);
            //to be implemented
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(string id)
        {
            Part a = _PartRepository.GetPart(id).Result;
            a.IsActive = false;
            await _PartRepository.UpdatePart(a.Id, a);
            return await Inventory();
        }

        public IActionResult edit()
        {
            PartViewModel model = new PartViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query, InventoryViewModel model)
        {
            var db = await _PartRepository.GetAllPart();
            var list = new List<Part>();
            if (!string.IsNullOrWhiteSpace(query))
            {
                foreach (var record in db)
                {
                    if (record.ToString().ToLower().Contains(query.ToLower()))
                    {
                        list.Add(record);
                    }
                }
                model.Parts = list;
            }
            if (model.Parts == null)
            {
                model.Parts = model.Parts ?? await _PartRepository.GetAllPart();
                model.Parts = model.Parts.ToList();
            }
            return InventorySearch(model);
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


