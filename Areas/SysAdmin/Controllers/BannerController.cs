using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Domain.AutoMapper;
using System.Linq.Expressions;
using DanaZhangCms.Domain.DbModels;
using DanaZhangCms.Helper;
using DanaZhangCms.DbModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.SysAdmin.Controllers
{
  
    [Area("SysAdmin")]
    [Authorize(Roles = "admin")]
    public class BannerController : Controller
    {
        private MyContext dbContext;
        public BannerController(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<JsonResult> IndexData(int page = 1, string key = "", int rows = 10)
        {
            Expression<Func<Banner, bool>> predicate = o => !o.IsDel;
            if (!string.IsNullOrWhiteSpace(key))
                predicate = predicate.And(s => s.Title.Contains(key));
            var query = dbContext.Banners.Where(predicate);
            var count = query.Count();
            var data = query.OrderByDescending(o => o.Id).Skip((page - 1) * rows).Take(rows).ToList();
            var pages = count % rows == 0 ? count / rows : count / rows + 1;
            return Json(new { records = count, page = page, total = pages, rows = data });
        }

        public async Task<IActionResult> Add()
        {
            var model = new Banner();
            var channelsList = this.dbContext.Channels.Where(o => !o.IsDel).ToList();
            ViewBag.ChannelsList = channelsList;
            return View(model);
        }


       



        [HttpPost]
        public async Task<IActionResult> Add(Banner model)
        {
            model.AddTime = System.DateTime.Now;
            this.dbContext.Banners.Add(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = this.dbContext.Banners.SingleOrDefault(o => o.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BannerViewModel modelDto)
        {
            var model = this.dbContext.Banners.SingleOrDefault(o => o.Id == id);
            model.MapFrom(modelDto);
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Banners.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            var model = this.dbContext.Banners.SingleOrDefault(o => o.Id == id);
            model.IsDel = true;
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Banners.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return Json(result);
        }


    }
}
