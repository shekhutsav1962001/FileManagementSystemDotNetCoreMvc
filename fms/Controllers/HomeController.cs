using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace fms.Controllers
{

    [Authorize(Roles = "user")]

    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private string email;



        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IHostingEnvironment hostingEnvironment, AppDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this._context = context;
        }
        
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                Console.WriteLine(User.Identity.Name);
            }
            return View();
        }

      
       

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Blocked()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (signInManager.IsSignedIn(User))
            {

                Console.WriteLine(User.Identity.Name);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileCreate model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;


                if (model.FileToUpload != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileToUpload.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileToUpload.CopyToAsync(stream);
                    }
                }
                if (signInManager.IsSignedIn(User))
                {
                    this.email = User.Identity.Name;
                    Console.WriteLine(User.Identity.Name);
                }

    
                Uploadfile obj = new Uploadfile
                {

                    Email = this.email,
                    filepath = uniqueFileName
                };
                _context.Add(obj);
                await _context.SaveChangesAsync();

                return View("index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            if (signInManager.IsSignedIn(User))
            {
                this.email = User.Identity.Name;

            }
            List<Uploadfile> fileList = new List<Uploadfile>();
            var a = await _context.files.ToListAsync();
            foreach (var item in a)
            {

                if (item.Email.Equals(this.email))
                {
                    fileList.Add(item);

                }
            }
            return View(fileList);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var file = await _context.files.FindAsync(id);

                if (file == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.files.Remove(file);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }


        }

        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var file = await _context.files.FindAsync(id);

                if (file == null)
                {
                    return NotFound();
                }
                else
                {

                    var path = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "wwwroot\\files");

                    path = path + "\\" + file.filepath;
                    Console.WriteLine(path);


                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    var ext = Path.GetExtension(path).ToLowerInvariant();
                    return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));
                }

            }


        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".css", "text/css"},
                {".html","text/html" },
                { ".js","text/javascript"},
                { ".json","application/json"},
                { ".mp3","audio/mpeg"},
                { ".php","application/x-httpd-php"},
                { ".ppt","application/vnd.ms-powerpoint"},
                { ".xml","application/xml"},
                {".zip","application/zip" }
            };
        }


        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
