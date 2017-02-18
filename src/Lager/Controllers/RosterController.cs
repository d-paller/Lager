using Lager.Interfaces;
using Lager.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Controllers
{
    public class RosterController : Controller
    {
        private IHostingEnvironment _environment;
        private IStudentRepository _studentRepo;

        public RosterController(IHostingEnvironment environment, IStudentRepository studentRepo)
        {
            _environment = environment;
            _studentRepo = studentRepo;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var path = Path.Combine(_environment.WebRootPath, "RosterUploads", file.FileName);

            if(file.Length > 0)
            {
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            StudentRoster roster = new StudentRoster(_studentRepo);
            await roster.AddStudentsToDb(path);

            return RedirectToAction("Index");
        }
    }
}
