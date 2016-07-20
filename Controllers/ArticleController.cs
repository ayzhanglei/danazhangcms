using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DanaZhangCms.Domain.DbModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Controllers
{
    public class ArticleController : Controller
    {
        private MyContext dbContext;
        public ArticleController(MyContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index(string category, int page = 1)
        {
            var cates = this.dbContext.Channels.FirstOrDefault(o => o.Name == category);
            if (cates == null)
                return Redirect("~/404.html");
            ViewBag.Title = cates.Title;
            var pageSize = 5;
            ViewBag.Cates = cates;
            ViewBag.Url = HttpContext.Request.Path.ToString();
            var query = (
             from a in this.dbContext.Articles
             join ac in this.dbContext.ArticleCategorys on a.CategoryId equals ac.Id
             join c in this.dbContext.Channels on ac.ChannelId equals c.Id
             select new Article()
             {
                 ImgUrl = a.ImgUrl,
                 ChannelId = c.Id,
                 Title = a.Title,
                 ZhaiYao = a.ZhaiYao,
                 Id = a.Id,
                 AddTime = a.AddTime
             }
             ).Where(o => o.ChannelId == cates.Id);//.OrderByDescending(o => o.AddTime).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var count = query.Count();
            ViewBag.PageIndex = page;
            ViewBag.PageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;
            var data = query.OrderByDescending(o => o.AddTime).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(data);
        }

        public IActionResult Content(int id)
        {
            var model = this.dbContext.Articles.FirstOrDefault(o => o.Id == id);
            if (model == null)
                return Redirect("~/404.html");
            ViewBag.Title = model.Title;
            model.Click += 1;
            this.dbContext.Articles.Attach(model);
            this.dbContext.SaveChangesAsync();
            return View(model);
        }

    }
}
