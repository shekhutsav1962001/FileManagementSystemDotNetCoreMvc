using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fms.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppDbContext _context;

        public AdministrationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

            this._context = context;
        }

        
        public IActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
           
            
           
            return View(await _context.files.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Alluser()
        {
            //await _context.Users.ToListAsync()
            return View(await _context.users.ToListAsync());
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
                    return RedirectToAction("index", "administration");
                    
                }
            }


        }


        public async Task<IActionResult> Unblock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _context.users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    user.blocked = false;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index", "administration");

                }
            }


        }


        public async Task<IActionResult> Block(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _context.users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    user.blocked = true;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index", "administration");

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


    }
}
