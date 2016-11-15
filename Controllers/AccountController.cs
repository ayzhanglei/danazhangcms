﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using DanaZhangCms.Domain.ViewModel;
using DanaZhangCms.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DanaZhangCms.DbModels;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _logger = logger;
        }



        //
        // GET: /Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.Title = "用户登录";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //admin123
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Logged in {userName}.", model.UserName);
                if (string.IsNullOrWhiteSpace(returnUrl))
                    return RedirectToAction("Index", "Home", new { area = "sysadmin" });
                return RedirectToLocal(returnUrl);
            }
            else
            {
                _logger.LogWarning("Failed to log in {userName}.", model.UserName);
                ModelState.AddModelError("", "用户名或密码错误");
                return View();
            }
        }

        //
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email, CreateOn = DateTime.Now, LastTime = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {userName} was created.", model.Email);
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await MessageServices.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;

            await SignInManager.SignOutAsync();

            _logger.LogInformation("{userName} logged out.", userName);
            return RedirectToAction("Index", "Home");
        }

        #region 辅助方法

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                _logger.LogWarning("Error in creating user: {error}", error.Description);
            }
        }

        private Task<User> GetCurrentUserAsync()
        {
            return UserManager.GetUserAsync(HttpContext.User);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
