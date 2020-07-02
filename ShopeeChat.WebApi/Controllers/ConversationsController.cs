using System;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ShopeeChat.WebApi.Controllers
{
    public class ConversationsController : V1Controller
    {
        private readonly IConversationsRepository _conversationsRepository;

        public ConversationsController(IConversationsRepository conversationsRepository)
        {
            _conversationsRepository = conversationsRepository;
        }

        [HttpGet("sync-conversations")]
        public async Task<IActionResult> SyncConversations()
        {
            var result = await _conversationsRepository.SyncConversations();
            return Ok(result);
        }

        [HttpGet("get-unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var result = await _conversationsRepository.GetUnreadCount();
            return Ok(result);
        }

        [HttpGet("get-all-conversation")]
        public async Task<IActionResult> GetAllConversation()
        {
            var result = await _conversationsRepository.GetAllConversation();
            return Ok(result);
        }

        [HttpGet("{conversationsId}/get-conversations-detail")]
        public async Task<IActionResult> GetConversationsDetail(string conversationsId)
        {
            try
            {
                var result = await _conversationsRepository.GetDetailConversation(conversationsId);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(ChatMessageRequest chatMessageRequest)
        {
            var result = await _conversationsRepository.SendChatMessage(chatMessageRequest);
            return Ok(result);
        }

        [HttpGet("get-new-message")]
        public async Task<IActionResult> GetNewMessage()
        {
            var result = await _conversationsRepository.GetNewMessage();
            return Ok(result);
        }
    }
}