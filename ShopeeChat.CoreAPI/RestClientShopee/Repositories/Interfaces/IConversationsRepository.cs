using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces
{
    public interface IConversationsRepository
    {
        Task<UnreadCountResponse> GetUnreadCount();
        Task<ResultConversationSyncResponse> SyncConversations();
        Task<List<ConversationResponse>> GetAllConversation();
        Task<List<ConversationDetailsResponse>> GetDetailConversation(string conversationId);
        Task<object> SendChatMessage(ChatMessageRequest chatMessageRequest);
        Task<List<NewMessageResponse>> GetNewMessage();
    }
}