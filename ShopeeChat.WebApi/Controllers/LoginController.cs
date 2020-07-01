using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using ShopeeChat.Infrastructure.Utility;
using ShopeeChat.WebApi.Data;
using ShopeeChat.WebApi.Data.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models.Base;

namespace ShopeeChat.WebApi.Controllers
{
    public class LoginController : V1Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ShopeeChatDbContext _shopeeChatDbContext;
        private readonly Random _random = new Random();


        public LoginController(ILoginRepository loginRepository, ShopeeChatDbContext shopeeChatDbContext)
        {
            _loginRepository = loginRepository;
            _shopeeChatDbContext = shopeeChatDbContext;
        }

        [HttpGet("get-data-login-page")]
        public async Task<LoginInterfaceResponse> GetDataLoginPage()
        {
            var result = await _loginRepository.GetLoginInterface();
            return result;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModelRequest loginModelRequest)
        {
            try
            {
                string passwordHash =
                    HashAlgorithms.GetSha256(HashAlgorithms.CreateMD5(loginModelRequest.Password).ToLower());
                object shopeeModelRequest = null;
                var checkUserName = CheckerUtility.CheckUserName(loginModelRequest.UserName);
                if (checkUserName.IsPhone)
                {
                    if (string.IsNullOrEmpty(loginModelRequest.VCode))
                    {
                        shopeeModelRequest = new LoginShopeeUsePhoneModelRequest()
                        {
                            Captcha = loginModelRequest.Captcha,
                            Remember = false,
                            Captcha_Key = loginModelRequest.CaptchaKey,
                            Password_Hash = passwordHash,
                            Phone = checkUserName.UserName
                        };
                    }
                    else
                    {
                        shopeeModelRequest = new LoginShopeeUseVCodeAndPhoneModelRequest()
                        {
                            Captcha = loginModelRequest.Captcha,
                            Remember = false,
                            Captcha_Key = loginModelRequest.CaptchaKey,
                            Password_Hash = passwordHash,
                            Phone = checkUserName.UserName,
                            VCode = loginModelRequest.VCode
                        };
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(loginModelRequest.VCode))
                    {
                        shopeeModelRequest = new LoginShopeeUseUserNameModelRequest()
                        {
                            Captcha = loginModelRequest.Captcha,
                            Remember = false,
                            Captcha_Key = loginModelRequest.CaptchaKey,
                            Password_Hash = passwordHash,
                            UserName = checkUserName.UserName
                        };
                    }
                    else
                    {
                        shopeeModelRequest = new LoginShopeeUseVCodeAndUserNameModelRequest()
                        {
                            Captcha = loginModelRequest.Captcha,
                            Remember = false,
                            Captcha_Key = loginModelRequest.CaptchaKey,
                            Password_Hash = passwordHash,
                            UserName = checkUserName.UserName,
                            VCode = loginModelRequest.VCode
                        };
                    }
                }

                var result = await _loginRepository.Login(shopeeModelRequest);
                if (result != null)
                {
                    // var currentUserId = User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                    var shopeeUser = new ShopeeUser()
                    {
                        Phone = result.Phone,
                        Userid = result.Id,
                        SpcEc = result.SpcEc,
                        // MembershipId = new Guid(currentUserId ?? string.Empty)
                    };
                    await _shopeeChatDbContext.ShopeeUsers.AddAsync(shopeeUser);
                    var resultCreate = await _shopeeChatDbContext.SaveChangesAsync();
                }

                return Ok(result);
            }
            catch (ApiException e)
            {
                return BadRequest(e.Error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await _loginRepository.RefreshToken();
            if (result != null)
            {
                var shopeeUser = _shopeeChatDbContext.ShopeeUsers
                    .FirstOrDefault(x => x.Userid == "20878264");
                if (shopeeUser != null)
                {
                    shopeeUser.UserName = result.User.Name;
                    shopeeUser.Avatar = result.User.Avatar;
                    shopeeUser.Locale = result.User.Locale;
                    shopeeUser.Token = result.Token;
                    shopeeUser.ShopId = result.User.ShopId;
                    _shopeeChatDbContext.ShopeeUsers.Update(shopeeUser);
                    await _shopeeChatDbContext.SaveChangesAsync();
                }
            }

            return Ok(result);
        }

        [HttpGet("get-captcha")]
        public async Task<IActionResult> GetCaptcha()
        {
            var captchaKey = new
            {
                captcha_key = _random.Next(1, 999999999).ToString()
            };
            var result = await _loginRepository.GetCaptcha(captchaKey);
            return File(result, "image/png");
        }

    }
}