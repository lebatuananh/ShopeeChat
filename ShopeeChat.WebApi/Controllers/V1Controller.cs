using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.Exceptions;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.WebApi.Data.Entities;
using System.Linq;

namespace ShopeeChat.WebApi.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class V1Controller : ControllerBase
    {
        public ApplicationUser CurrentUser => HttpContext.User.CurrentUser();
    }
}