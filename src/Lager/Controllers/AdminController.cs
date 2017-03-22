
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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lager.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPartRepository _PartRepository;
        private readonly IStudentRepository _studentRepo;

        private IEnumerable<Student> _studentList;

        private User user = new User() { IsActive = true, Name = "Test", Username = "TestUserName", Admin = true };

        public AdminController(IPartRepository partRepository, IStudentRepository studentRepo)
        {
            _PartRepository = partRepository;
            _studentRepo = studentRepo;

            _studentList = Task.Run(() => _studentRepo.GetStudents()).Result;
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

            var Db = await _PartRepository.GetAllPart();
            var DbList = Db.ToList();
            var DbCount = DbList.Count();

            pagingInfo.PageCount = DbCount % pagingInfo.PageSize > 0 ?
                DbCount / pagingInfo.PageSize + 1 :
                DbCount / pagingInfo.PageSize;

            pagingInfo.CurrentPageIndex = 0;
            pagingInfo.SortField = "Category";

            model.PagingInfo = pagingInfo;
            model.Parts = DbList.Take(pagingInfo.PageSize);

            model.StudentSelectList = _studentList;

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

            model.StudentSelectList = _studentList;

            model.Parts = query.ToList();
            return View(model);
        }


        public IActionResult InventorySearch(InventoryViewModel model)
        {
            IQueryable<Part> query = model.Parts.AsQueryable();

            if (model.Parts.Count() != 0)
            {
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

                var count = model.Parts.Count();
                model.PagingInfo.PageCount = count % model.PagingInfo.PageSize > 0 ?
                        count / model.PagingInfo.PageSize + 1 :
                        count / model.PagingInfo.PageSize;

                query = query.Skip(model.PagingInfo.CurrentPageIndex * model.PagingInfo.PageSize)
                    .Take(model.PagingInfo.PageSize);

                model.StudentSelectList = _studentList;

                model.Parts = query.ToList();
            }
            return View("Inventory", model);
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

        public async Task<IActionResult> EditItem(string name, int id)
        {
            Part a = _PartRepository.GetPart(name, id).Result;
            await _PartRepository.UpdatePart(a.Id, a);
            return RedirectToAction("Inventory");

        }

        [HttpPost]
        public async Task<ActionResult> RemoveItem(string name, int id)
        {
            Part a = _PartRepository.GetPart(name, id).Result;
            a.IsActive = false;
            await _PartRepository.UpdatePart(a.Id, a);
            return View();
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
            if(model.Parts == null)
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


