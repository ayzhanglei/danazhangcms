using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Helper
{
    public class CookieHelper
    {
        public static void SetCookie(HttpContext context, string name, string value, double expires = 0)
        {
            var CookieOption = new CookieOptions();
            CookieOption.Path = "/";
            //on localhost, Domain value is not needed !!
            CookieOption.Domain = "";
            CookieOption.Expires = DateTime.Now.AddHours(1);
            CookieOption.HttpOnly = true;
            if (expires > 0)
                CookieOption.Expires = DateTime.Now.AddMinutes(expires);
            context.Response.Cookies.Append(name, value, CookieOption);
        }

        public static string GetCookie(HttpContext context, string name)
        {
            var cookie = context.Request.Cookies[name];
            return cookie;
        }
    }
}
