using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.WebApplication.Models;

namespace ShopeeChat.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConversationsRepository _conversationsRepository;

        public HomeController(ILogger<HomeController> logger, IConversationsRepository conversationsRepository)
        {
            _logger = logger;
            _conversationsRepository = conversationsRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}