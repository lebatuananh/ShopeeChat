using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;

namespace ShopeeChat.Demo.WebApplication.Controllers.Components
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