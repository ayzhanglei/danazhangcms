using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DanaZhangCms.DbModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DanaZhangCms.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DanaZhangCms.Extensions;
using DanaZhangCms.Domain.DbModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace DanaZhangCms
{
    #region 频道信息
    /// <summary>
    /// 频道匹配
    /// </summary>
    public class ChannelConstraint : IRouteConstraint
    {
        public List<DanaZhangCms.Domain.DbModels.Channel> cates = new List<DanaZhangCms.Domain.DbModels.Channel>();
        public ChannelConstraint(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<MyContext>();
                cates = db.Channels.ToList();
            }
        }
        public bool Match(Microsoft.AspNetCore.Http.HttpContext httpContext, IRouter route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey("category"))
            {
                var cate = values["category"].ToString().Trim().ToLower();
                var result = cates.Any(s => s.Name.Trim().ToLower() == cate);
                return result;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion
    public class Startup
    {
        private readonly IHostingEnvironment hostingEnvironments;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            // Below code demonstrates usage of multiple configuration sources. For instance a setting say 'setting1'
            // is found in both the registered sources, then the later source will win. By this way a Local config
            // can be overridden by a different setting while deployed remotely.
            hostingEnvironments = hostingEnvironment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("config.json")
                //All environment variables in the process's context flow in as configuration values.
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            GlobalConfiguration.ApplicationPath = hostingEnvironments.WebRootPath;

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings")); //需要引用 Microsoft.Extensions.Options.ConfigurationExtensions
            //var connectionStringKey = "Data__DefaultConnection__ConnectionString"; ;
            //var conn = Configuration[connectionStringKey.Replace("__", ":")];
            services.AddDbContext<MyContext>(options => options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]));        
            services.AddIdentity<User, Role>(configure =>
            {
                configure.Password = new PasswordOptions()
                {
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
                configure.Cookies.ApplicationCookie.LoginPath = "/account/login";
            })
              .AddEntityFrameworkStores<MyContext, long>()
              .AddDefaultTokenProviders();

            services.AddScoped<SignInManager<User>, DanaZhangCmsSignInManager<User>>();
            services.AddMvc();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            #region 注入
            services.AddScoped<IWorkContext, WorkContext>();
            #endregion

            #region 注入
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Configuration);
            builder.RegisterInstance(hostingEnvironments);
            builder.RegisterMediaType(Configuration["Storage:StorageType"]);
            #endregion
            builder.Populate(services);//将现有的Services路由到Autofac的管理集合中
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();// 返回AutoFac实现的IServiceProvider

        }




        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddNLog();
            DanaZhangCms.Domain.AutoMapper.Configuration.RegisterConfigure();
            // app.UseAutoMapper();
            //启用session
            // app.UseSession();
            //启用静态文件支持
            // app.UseStaticFiles();

            InitializeDatabase(app);
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseStatusCodePages();

            ////Solving HTTP 500 errors
            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("~/404.html");
            //}

            // app.UseMvc();
            // 启用 Mvc 并定义默认的路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ArticleList",
                    template: "{category}.html",
                    defaults: new { controller = "Article", action = "Index", category="" },
                    constraints: new { customConstraint = new ChannelConstraint(app) });
                routes.MapRoute(
                   name: "ArticleListNoHtml",
                   template: "{category}",
                   defaults: new { controller = "Article", action = "Index", category = "" },
                   constraints: new { customConstraint = new ChannelConstraint(app) });
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { action = "Index", controller = "Home" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");



            });





        }





        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<MyContext>();
                if (db.Database.EnsureCreated())
                {
                    db.Roles.Add(new Role() { Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = "bd3bee0b-5f1d-482d-b890-ffdc01915da3" });
                    db.Roles.Add(new Role() { Name = "user", NormalizedName = "USER", ConcurrencyStamp = "bd3bee0b-5f1d-482d-b890-ffdc01915db3" });
                    db.Roles.Add(new Role() { Name = "guest", NormalizedName = "GUEST", ConcurrencyStamp = "bd3bee0b-5f1d-482d-b890-ffdc01915dj3" });
                    db.Channels.Add(new Channel() { AddTime = System.DateTime.Now, Name = "anli", Title = "客户案例", IsAlbums = true, SortId = 1 });
                    db.Channels.Add(new Channel() { AddTime = System.DateTime.Now, Name = "servicecenter", Title = "服务中心", IsAlbums = true, SortId = 2 });
                    db.Channels.Add(new Channel() { AddTime = System.DateTime.Now, Name = "xinwen", Title = "新闻动态", IsAlbums = true, SortId = 3 });
                    db.Channels.Add(new Channel() { AddTime = System.DateTime.Now, Name = "aboutus", Title = "关于我们", IsAlbums = true, SortId = 4 });
                    db.Channels.Add(new Channel() { AddTime = System.DateTime.Now, Name = "recruit", Title = "招贤纳士", IsAlbums = true, SortId = 5 });


                    db.SaveChanges();
                    var usersql = "INSERT INTO User ( AccessFailedCount, Avatar, ConcurrencyStamp, CreateOn, Email, EmailConfirmed, LastTime, LockoutEnabled, LockoutEnd, NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, Profile, SecurityStamp, Tel, TwoFactorEnabled, UserGuid, UserName) VALUES ( 0, NULL, 'aa32803c-c29b-40cc-a4c6-758c08247b75', '2016-07-15 10:12:42.4388955', 'ayzhanglei@gmail.com', 0, '2026-07-15 10:12:42.4388955', 1, NULL, 'AYZHANGLEI@GMAIL.COM', 'ADMIN', 'AQAAAAEAACcQAAAAEFh2vCrLc2qY4QZhkzPoPBeCUbFS0xeceRbQzMYVyYn+CyWZM1G73xlhn4mnjYnKQQ==', NULL, 0, NULL, 'f2ee7ff4-1c78-4877-a78b-520065063942', NULL, 0, '47fc8c2e-e078-4740-861a-b0b354dce270', 'admin');";

                    db.Database.ExecuteSqlCommand(usersql);
                    db.Database.ExecuteSqlCommand("INSERT INTO UserRole (UserId, RoleId) VALUES (1, 1);");




                }
            }
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var db = serviceScope.ServiceProvider.GetService<MyContext>();

            //    if (db.Database.EnsureCreated())
            //    {
            //        var UserManager = new Microsoft.AspNetCore.Identity.UserManager<Manager>();
            //        var user = new Manager { UserName = "admin", Email = "admin@admin.com", AddTime = DateTime.Now, LastTime = DateTime.Now };
            //        var result = await UserManager.CreateAsync(user, model.Password);

            //        //var manageModel = new manager() { UserName = "admin", AddTime = System.DateTime.Now, PasswordHash = "123456" };
            //        //db.manager.Add(manageModel);
            //        //db.SaveChanges();
            //    }
            //}
        }




    }
}