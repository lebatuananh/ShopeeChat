using ShopeeChat.RestClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;

namespace ShopeeChat.RestClient.Repositories.Interfaces
{
    public interface IConversationsRepository
    {
        Task<List<ConversationResponse>> GetAllConversation();

        Task<List<ConversationDetailsResponse>> GetDetailConversation(string conversationId);

        Task<UnreadCountResponse> GetUnreadCount();
        Task<ResultConversationSyncResponse> SyncConversations();
        Task<object> SendChatMessage(ChatMessageRequest chatMessageRequest);
        Task<List<NewMessageResponse>> GetNewMessage();

    }
}