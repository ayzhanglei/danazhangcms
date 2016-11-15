using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DanaZhangCms.Framework;
using Microsoft.AspNetCore.Identity;
using DanaZhangCms.DbModels;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.Home.Controllers
{
    [Area("SysAdmin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
         public SignInManager<User> SignInManager { get; }
       
       public HomeController(SignInManager<User> signInManager)
        {            
            SignInManager = signInManager;           
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Center()
        {
            return View();
        }

         public async Task<ActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
