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

        public RosterController(IHostingEnvironment environment)
        {
            _environment = environment;
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
            var uploads = Path.Combine(_environment.WebRootPath, "RosterUploads");

            if(file.Length > 0)
            {
                using(var fileStream = new FileStream(Path.Combine(uploads, file.FileName),
                    FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
