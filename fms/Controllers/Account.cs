using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fms.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _context;

        public Account(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {


                    // await signInManager.SignInAsync(user, isPersistent: false);

                    //var usr = await userManager.FindByIdAsync(model.Email);
                    var myuser = new User
                    {
                        Email = model.Email,
                        blocked=false
                    };
                    _context.Add(myuser);
                    await _context.SaveChangesAsync();

                    IdentityResult rslt = null;

                    if (!(await userManager.IsInRoleAsync(user, "user")))
                    {
                        rslt = await userManager.AddToRoleAsync(user, "user");
                    }


                  

                    return RedirectToAction("login", "account");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
           //     var tempuser = _context.users.SingleOrDefault(x => x.Email == model.Email);
            //    Console.WriteLine("temp");
             //   Console.WriteLine(tempuser.blocked);

                



                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                Console.WriteLine(model.Email);
                var temp = "admin@gmail.com";
                

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        if (temp.Equals(model.Email))
                        {
                            Console.WriteLine("ya admin");
                            return RedirectToAction("index", "administration");
                        }
                        else
                        {
                            var tempuser = _context.users.SingleOrDefault(x => x.Email == model.Email);
                            Console.WriteLine("temp1");
                            Console.WriteLine(tempuser.blocked);
                            if(tempuser.blocked)
                            {
                                Console.WriteLine("yes blocked1");
                                await signInManager.SignOutAsync();
                                return RedirectToAction("blocked", "home");
                            }   
                            else
                            {
                                return Redirect(returnUrl);
                            }


                            
                        }

                        
                    }
                    else
                    {
                        if(temp.Equals(model.Email))
                        {
                            return RedirectToAction("index", "administration");
                        }
                        else
                        {
                            var tempuser = _context.users.SingleOrDefault(x => x.Email == model.Email);
                            Console.WriteLine("temp2");
                            Console.WriteLine(tempuser.blocked);


                            if (tempuser.blocked)
                            {
                                Console.WriteLine("yes blocked2");
                                await signInManager.SignOutAsync();
                                return RedirectToAction("blocked", "home");
                            }
                            else
                            {
                                return RedirectToAction("index", "home");
                            }
                        }
                      
                    }
                    
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
    }
}
