using DanaZhangCms.DbModels;
using DanaZhangCms.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Framework
{

    public class BaseManageController : Controller
    {

        public IWorkContext _workContext { get; set; }
        public User OnLineUser;
        public BaseManageController()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                 .AddSingleton<IWorkContext, WorkContext>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            //IServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();
            _workContext = serviceProvider.GetService<IWorkContext>();

           // OnLineUser = await _workContext.GetCurrentUser();

        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        protected string GetRouteString(string key, string defaultValue)
        {
            object value = RouteData.Values[key];
            if (value != null)
                return value.ToString();
            else
                return defaultValue;
        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        protected string GetRouteString(string key)
        {
            return GetRouteString(key, "");
        }



    }
}
