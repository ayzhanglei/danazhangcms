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
    [Area("sysadmin")]
    [Authorize(Roles = "admin")]
    public class ArticleCategoryController : Controller
    {
        private MyContext dbContext;
        public ArticleCategoryController(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<JsonResult> IndexData(int page = 1, string key = "", int pageSize = 10)
        {
            Expression<Func<ArticleCategory, bool>> predicate = o => !o.IsDel;
            if (!string.IsNullOrWhiteSpace(key))
                predicate = predicate.And(s => s.Title.Contains(key));
            var query = dbContext.ArticleCategorys.Where(predicate);
            var count = query.Count();
            var data = query.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var pages = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;
            return Json(new { records = count, page = page, total = pages, row = data });
        }

        public async Task<IActionResult> Add()
        {
            var model = new ArticleCategory();
            var channelsList = this.dbContext.Channels.Where(o => !o.IsDel).ToList();
            ViewBag.ChannelsList = channelsList;
            return View(model);
        }


        public async Task<IActionResult> CategoryJson()
        {

            var articleCategoryList = this.dbContext.ArticleCategorys.ToList();
            var model = SetCateJson(articleCategoryList);
            return Json(model);
        }

        public List<AddCategoryViewModel> SetCateJson(List<ArticleCategory> list)
        {
            var model = new List<AddCategoryViewModel>();
            var parentList = list.Where(o => o.ParentId == 0).ToList();
            model.Add(new AddCategoryViewModel() { text = "无父级节点", href = "0", tags = new List<string>() { "" + 0 } });
            foreach (var item in parentList)
            {
                var childItem = item.Id == 1 ? null : SetChildCateJson(list.Where(o => o.Id != item.Id && o.ParentId == item.Id).ToList(), item.Id);
                model.Add(new AddCategoryViewModel() { text = item.Title, href = "" + item.Id, tags = new List<string>() { "" + list.Count(o => o.ParentId == item.Id && o.Id != item.Id) }, nodes = childItem });
            }

            return model;
        }

        public List<AddCategoryViewModel> SetChildCateJson(List<ArticleCategory> list, int parent = 1)
        {
            var model = new List<AddCategoryViewModel>();
            foreach (var item in list)
            {
                var childItem = SetChildCateJson(list.Where(o => o.Id != item.Id && o.ParentId == item.Id).ToList(), item.ParentId);
                model.Add(new AddCategoryViewModel() { text = item.Title, href = "" + item.Id, tags = new List<string>() { "" + list.Count(o => o.Id != item.Id && o.ParentId == item.Id) }, nodes = childItem });
            }
            return model;
        }



        [HttpPost]
        public async Task<IActionResult> Add(ArticleCategory model)
        {
            model.AddTime = System.DateTime.Now;
            this.dbContext.ArticleCategorys.Add(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = this.dbContext.ArticleCategorys.SingleOrDefault(o => o.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ArticleCategoryViewModel modelDto)
        {
            var model = this.dbContext.ArticleCategorys.SingleOrDefault(o => o.Id == id);
            model.MapFrom(modelDto);
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.ArticleCategorys.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            var model = this.dbContext.ArticleCategorys.SingleOrDefault(o => o.Id == id);
            model.IsDel = true;
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.ArticleCategorys.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return Json(result);
        }


    }
}
