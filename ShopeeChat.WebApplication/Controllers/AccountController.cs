using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreApplication.Statics;
using ShopeeChat.RestClient.Models.Base;
using ShopeeChat.RestClient.Models.Requests;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.WebApplication.Models;

namespace ShopeeChat.WebApplication.Controllers
{
    public class AccountController : BaseController
    {
        private IUserSystemRepository _userSystemRepository;

        public AccountController(IUserSystemRepository userSystemRepository)
        {
            _userSystemRepository = userSystemRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSystemLoginRequest model)
        {
            CommonResultModel objMsg = new CommonResultModel();
            if (ModelState.IsValid)
            {
                var userLogin = await _userSystemRepository.LoginSystem(model);
                if (userLogin != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    //objMsg.Error = false;
                    //objMsg.Title = "/";
                    return Redirect("/");
                }
                else
                {
                    objMsg.Error = true;
                    objMsg.Title = Notify.ExistsErorr;
                }
            }
            return Json(objMsg);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
