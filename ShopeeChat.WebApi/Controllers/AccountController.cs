using DVG.AutoPortal.InfrastructureLayer.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShopeeChat.CoreAPI.Statics;
using ShopeeChat.Infrastructure.Extensions;
using ShopeeChat.Infrastructure.Utility;
using ShopeeChat.WebApi.Data.Entities;
using ShopeeChat.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.WebApi.Controllers
{
    public class AccountController : V1Controller
    {
        private readonly UserManager<Membership> _userManager;
        private readonly SignInManager<Membership> _signInManager;

        public AccountController(UserManager<Membership> userManager, SignInManager<Membership> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                {
                    return await BuildToken(user);
                }
            }
            return BadRequest("Please try another email address or password.");
        }

        [Authorize]
        [HttpGet("test-auth")]
        public async Task<IActionResult> TestFunc()
        {
            var abc = CurrentUser;
            return Ok(JsonConvert.SerializeObject(abc));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountRegisterModel request)
        {
            var user = new Membership
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Created($"User/{user.Id}", null);
            }
            return BadRequest(result.GetError());
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Please try another email address.");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = UrlBuilder.ResetPasswordCallbackLink(code);
            //await _emailSender.SendEmailAsync(request.Email, "Reset Password",
            //    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Ok("Please check your email to reset your password.");
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Please try another email address.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);
            if (result.Succeeded)
            {
                return Ok("Your password has been reset.");
            }

            return BadRequest(result.GetError());
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for email confirm.");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok("Thank you for confirming your email.");
            }

            return BadRequest(result.GetError());
        }

        private async Task<IActionResult> BuildToken(Membership model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
                        {
                            new Claim(nameof(user.Id),CryptoEngine.Encrypt(user.Id,ApiStaticVariables.CommonSalt)),
                            new Claim(nameof(user.Email),CryptoEngine.Encrypt(user.Email,ApiStaticVariables.CommonSalt)),
                            new Claim(nameof(user.UserName),CryptoEngine.Encrypt(user.UserName,ApiStaticVariables.CommonSalt)),
                            new Claim(nameof(roles), CryptoEngine.Encrypt(string.Join(",",roles),ApiStaticVariables.CommonSalt))
                        };
            var securityKey = new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(ApiStaticVariables.AuthConfig.Key));
            var expires = DateTime.Now.AddHours(120);
            var securityToken = new JwtSecurityToken(
                issuer: ApiStaticVariables.AuthConfig.Issuer,
                audience: ApiStaticVariables.AuthConfig.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expires
            });
        }
    }
}