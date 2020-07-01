using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Models.Base;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.WebApplication.Models;
using System;
using System.Threading.Tasks;

namespace ShopeeChat.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly Random _random = new Random();

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        //[Route("login-shopee.html", Name = "Login")]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[Route("login-shopee.html", Name = "Login")]
        public async Task<IActionResult> Index(LoginModelRequest model, string returnUrl = null)
        {
            var loginResponse = new LoginShopeeResponseViewModel();
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var result = await _loginRepository.Login(model);
                    if (result != null)
                    {
                        return Redirect("conversations");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(loginResponse);
                    }
                }

                return View(loginResponse);
            }
            catch (ClientException e)
            {
                loginResponse.UserName = model.UserName;
                loginResponse.Password = model.Password;
                loginResponse.ErrorModel = e.Error;
                loginResponse.CaptchaKey = _random.Next(1, 999999999).ToString();
                return View(loginResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}