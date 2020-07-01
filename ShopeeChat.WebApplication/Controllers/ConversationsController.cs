using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.Infrastructure.Extensions;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.WebApplication.Models;

namespace ShopeeChat.WebApplication.Controllers
{
    [Authorize]
    public class ConversationsController : Controller
    {
        private readonly IConversationsRepository _conversationsRepository;

        public ConversationsController(IConversationsRepository conversationsRepository)
        {
            _conversationsRepository = conversationsRepository;
        }

        [Route("Conversations/Index/{conversationId}")]
        [Route("Conversations")]
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

        [HttpPost]
        public async Task<object> SendMessage([FromBody] ChatMessageRequest chatMessageRequest)
        {
            var result = await _conversationsRepository.SendChatMessage(chatMessageRequest);
            return result;
        }

        [HttpGet]
        public async Task<List<NewMessageResponse>> GetNewMessage()
        {
            var result = await _conversationsRepository.GetNewMessage();
            return result;
        }
    }
}