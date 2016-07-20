using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DanaZhangCms.Domain.DbModels;
using DanaZhangCms.Domain.AutoMapper;
using System.Linq.Expressions;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Helper;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.SysAdmin.Controllers
{
    [Area("sysadmin")]
    [Authorize(Roles = "admin")]
    public class ArticleController : Controller
    {
        private MyContext dbContext;
        public ArticleController(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<JsonResult> IndexData(int page = 1, string key = "", int pageSize = 10)
        {
            Expression<Func<Article, bool>> predicate = o => !o.IsDel;
            if (!string.IsNullOrWhiteSpace(key))
                predicate = predicate.And(s => s.Title.Contains(key));
            var query = dbContext.Articles.Where(predicate);
            var count = query.Count();
            var data = query.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var artIDs = data.Select(o => o.CategoryId).ToList();
            var categorys = this.dbContext.ArticleCategorys.Where(o => artIDs.Contains(o.Id)).ToList();
            foreach(var item in data)
            {
                item.Category = categorys.FirstOrDefault(o=>o.Id==item.CategoryId);
            }
            var pages = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;
            return Json(new { records = count, page = page, total = pages, row = data });
        }

        public async Task<IActionResult> Add()
        {
            var model = new ArticleViewModel();
            var channelsList = this.dbContext.Channels.Where(o => !o.IsDel).ToList();
            var category = this.dbContext.ArticleCategorys.Where(o => !o.IsDel).ToList();
            ViewBag.ChannelsList = channelsList;
            ViewBag.Category = category;
            return View(model);
        }






        [HttpPost]
        public async Task<IActionResult> Add(ArticleViewModel modelDto)
        {
            
            if (!ModelState.IsValid)
            {
                var channelsList = this.dbContext.Channels.Where(o => !o.IsDel).ToList();
                var category = this.dbContext.ArticleCategorys.Where(o => !o.IsDel).ToList();
                ViewBag.ChannelsList = channelsList;
                ViewBag.Category = category;
                return View(modelDto);
            }
            var model = new Article();
            model.MapFrom(modelDto);
            model.AddTime = System.DateTime.Now;
            this.dbContext.Articles.Add(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = this.dbContext.Articles.SingleOrDefault(o => o.Id == id);
            //var channelsList = this.dbContext.Channels.Where(o => !o.IsDel).ToList();
            var category = this.dbContext.ArticleCategorys.Where(o => !o.IsDel).ToList();
            //ViewBag.ChannelsList = channelsList;
            ViewBag.Category = category;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ArticleViewModel modelDto)
        {
            var model = this.dbContext.Articles.SingleOrDefault(o => o.Id == id);
            model.MapFrom(modelDto);
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Articles.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            var model = this.dbContext.Articles.SingleOrDefault(o => o.Id == id);
            model.IsDel = true;
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Articles.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return Json(result);
        }
    }
}
