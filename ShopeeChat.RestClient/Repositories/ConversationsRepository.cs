using System;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.RestClient.RestClients;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;

namespace ShopeeChat.RestClient.Repositories
{
    public class ConversationsRepository : BaseRepository, IConversationsRepository
    {
        public ConversationsRepository(ApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<List<ConversationResponse>> GetAllConversation()
        {
            var result = await ApiClient.GetAsync<List<ConversationResponse>>(ApiUrls.PathApiClientGetAllConversation);
            return result;
        }

        public async Task<List<ConversationDetailsResponse>> GetDetailConversation(string conversationId)
        {
            try
            {
                var result = await ApiClient.GetAsync<List<ConversationDetailsResponse>>(string.Format(ApiUrls.PathApiClientGetDetailConversation, conversationId));
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<UnreadCountResponse> GetUnreadCount()
        {
            var result = await ApiClient.GetAsync<UnreadCountResponse>(ApiUrls.PathApiClientGetUnreadCount);
            return result;
        }

        public async Task<ResultConversationSyncResponse> SyncConversations()
        {
            var result = await ApiClient.GetAsync<ResultConversationSyncResponse>(ApiUrls.PathApiClientSyncConversations);
            return result;
        }

        public async Task<object> SendChatMessage(ChatMessageRequest chatMessageRequest)
        {
            var result = await ApiClient.PostAsync<object>(ApiUrls.PathApiClientSendChatMessage, chatMessageRequest);
            return result;
        }

        public async Task<List<NewMessageResponse>> GetNewMessage()
        {
            var result = await ApiClient.GetAsync<List<NewMessageResponse>>(ApiUrls.PathApiClientGetNewMessage);
            return result;
        }
    }
}