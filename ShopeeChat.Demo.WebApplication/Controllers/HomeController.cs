using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeChat.Demo.WebApplication.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;

namespace ShopeeChat.Demo.WebApplication.Controllers
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

        [Route("Home/Index/{conversationId}")]
        [Route("")]
        public async Task<IActionResult> Index(string conversationId)
        {
            var conversations = await _conversationsRepository.GetAllConversation();
            var conversationDetails = new List<ConversationDetailsResponse>();
            var result = new ConversationsViewModel()
            {
                ConversationResponses = conversations
            };
            if (conversationId == null)
            {
                var conversation = conversations.FirstOrDefault();
                if (conversation != null)
                {
                    result.AvatarCustomer = conversation.ToAvatar;
                    result.AvatarCustomer = conversation.ToAvatar;
                    result.NameCustomer = conversation.ToName;
                    result.CustomerId = conversation.ToId;
                    result.SellerId = 20878264;
                    conversationDetails = await _conversationsRepository.GetDetailConversation(conversation.Id);
                }
            }
            else
            {
                var conversation = conversations.FirstOrDefault(x => x.Id == conversationId.ToString());
                if (conversation != null)
                {
                    result.AvatarCustomer = conversation.ToAvatar;
                    result.NameCustomer = conversation.ToName;
                    result.CustomerId = conversation.ToId;
                    result.SellerId = 20878264;
                    conversationDetails = await _conversationsRepository.GetDetailConversation(conversation.Id);
                }
            }
            conversationDetails.Reverse();
            result.ConversationDetailsResponses = conversationDetails;
            return View(result);
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