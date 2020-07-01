using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories;
using ShopeeChat.RestClient.Repositories.Interfaces;

namespace ShopeeChat.WebApplication.Controllers.Components
{
    public class ListConversationsViewComponent : ViewComponent
    {
        private readonly IConversationsRepository _conversationsRepository;

        public ListConversationsViewComponent(IConversationsRepository conversationsRepository)
        {
            _conversationsRepository = conversationsRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<ConversationResponse> conversationResponses)
        {
            return View(conversationResponses);
        }

    }
}