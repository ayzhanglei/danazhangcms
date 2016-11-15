using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DanaZhangCms.Domain.DbModels;
using System.Linq.Expressions;
using DanaZhangCms.Helper;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Domain.AutoMapper;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize(Roles = "admin")]
    public class ChannelController : Controller
    {
        private MyContext dbContext;
        public ChannelController(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<JsonResult> IndexData(int page = 1, string key = "", int rows = 10)
        {
            Expression<Func<Channel, bool>> predicate = o => !o.IsDel;
            if (!string.IsNullOrWhiteSpace(key))
                predicate = predicate.And(s => s.Name.Contains(key));
            var query = dbContext.Channels.Where(predicate);
            var count = query.Count();
            var data = query.OrderByDescending(o => o.Id).Skip((page - 1) * rows).Take(rows).ToList();
            var pages = count % rows == 0 ? count / rows : count / rows + 1;
            return Json(new { records = count, page = page, total = pages, rows = data });
        }

        public async Task<IActionResult> Add()
        {
            var model = new Channel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Channel model)
        {
            model.AddTime = System.DateTime.Now;
            this.dbContext.Channels.Add(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = this.dbContext.Channels.SingleOrDefault(o => o.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,ChannelViewModel modelDto)
        {
            var model = this.dbContext.Channels.SingleOrDefault(o => o.Id == id);
          
            model.MapFrom(modelDto);
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Channels.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            var model = this.dbContext.Channels.SingleOrDefault(o => o.Id == id);
            model.IsDel = true;
            model.UpdateTime = System.DateTime.Now;
            this.dbContext.Channels.Attach(model);
            var result = await this.dbContext.SaveChangesAsync() > 0;
            return Json(result);
        }

    }
}
