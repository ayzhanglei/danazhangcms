using DanaZhangCms.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Extensions
{
    public class WorkContext : IWorkContext
    {
        private const string UserGuidCookiesName = "DanaZhangCmsUserGuid";
        private const long GuestRoleId = 3;

        private User _currentUser;
        private UserManager<User> _userManager;
        private HttpContext _httpContext;
        private MyContext _dbContext;

        public WorkContext(UserManager<User> userManager, IHttpContextAccessor contextAccessor, MyContext dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContext = contextAccessor.HttpContext;
        }

        public async Task<User> GetCurrentUser()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            // On external login callback Identity.IsAuthenticated = true. But it's an external claim principal
            // Login by google, get _userManager.GetUserAsync from ClaimsPrincipal throw exception becasue the UserIdClaimType has value but too big.
            if (_httpContext.User.Identity.AuthenticationType == "Identity.Application")
            {
                var contextUser = _httpContext.User;
                _currentUser = await _userManager.GetUserAsync(contextUser);
            }
            if (_currentUser != null)
            {
                return _currentUser;
            }


            var userGuid = GetUserGuidFromCookies();
            if (!string.IsNullOrWhiteSpace(userGuid))
            {
                _currentUser = this._dbContext.Users.FirstOrDefault(x => x.UserGuid.ToString() == userGuid);
            }
            userGuid = Guid.NewGuid().ToString();
            var dummyEmail = string.Format("{0}@guest.danazhangcms.com", userGuid);
            _currentUser = new User
            {
                UserGuid = userGuid.ToString(),
                Email = dummyEmail,
                UserName = dummyEmail
            };
            var abc = await _userManager.CreateAsync(_currentUser, "1qazZAQ!");
            await _userManager.AddToRoleAsync(_currentUser, "guest");
     

            return _currentUser;



        }

        private string GetUserGuidFromCookies()
        {
            if (_httpContext.Request.Cookies.ContainsKey(UserGuidCookiesName))
            {
                return _httpContext.Request.Cookies[UserGuidCookiesName].ToString();
            }

            return "";
        }

        private void SetUserGuidCookies(Guid userGuid)
        {
            _httpContext.Response.Cookies.Append(UserGuidCookiesName, _currentUser.UserGuid.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(5),
                HttpOnly = true
            });
        }
    }
}
