using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DanaZhangCms.Framework;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.Home.Controllers
{
    [Area("sysadmin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
       
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
    }
}
