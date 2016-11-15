using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using DanaZhangCms.Extensions;
using DanaZhangCms.Framework;
using DanaZhangCms;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Domain.DbModels;
using DanaZhangCms.Services;
using DanaZhangCms.Controllers.Base;

public class HomeController : BaseController
{

    public IWorkContext _workContext { get; set; }
    private MyContext _dbContext;
    public HomeController(MyContext dbContext, IWorkContext workContext)
    {
        this._dbContext = dbContext;
        _workContext = workContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
     
       
        var model = new HomeIndexViewModel();
        var banner = this._dbContext.Banners.Where(o => !o.IsDel).OrderBy(o => o.SortId).ThenByDescending(o => o.Id).ToList();
        model.Banners = banner;

        var query = (
            from a in this._dbContext.Articles
            join ac in this._dbContext.ArticleCategorys on a.CategoryId equals ac.Id
            join c in this._dbContext.Channels on ac.ChannelId equals c.Id
            select new Article()
            {
                ImgUrl = a.ImgUrl,
                ChannelId = c.Id,
                Title = a.Title,
                ZhaiYao = a.ZhaiYao,
                Id = a.Id,
                AddTime = a.AddTime
            }
            );
        model.Anli = query.Where(o => o.ChannelId == 1).OrderByDescending(o=>o.Id).Take(8).ToList();
        model.News = query.Where(o => o.ChannelId == 3).OrderByDescending(o => o.Id).Take(9).ToList();
        return View(model);
    }

    public IActionResult NewsContent(int id)
    {
        //var model =  dbContext.Articles.FirstOrDefault(o=>o.Id.Equals(id));

        return View();
    }

    // [HttpGet("/about")]
    public IActionResult About()
    {
        ViewBag.About = "Hi";
        return View();
    }
}
