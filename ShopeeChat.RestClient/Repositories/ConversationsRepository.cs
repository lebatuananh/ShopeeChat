using System;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.RestClient.RestClients;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
                foreach (var r in result)
                {
                    if (!string.IsNullOrEmpty(r.Content.StickerId))
                    {
                        var cdn = StickerUrls.FirstOrDefault(x => x.Contains(r.Content.StickerPackageId));
                        r.Content.StickerUrl = $"{cdn.Substring(0, cdn.LastIndexOf("/") + 1)}{r.Content.StickerId}@1x.png";
                    }
                }
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

        public async Task<IList<string>> GetStickers()
        {
            var result = await Task.WhenAll(StickerUrls.Select(item =>
            {
                Task<IList<string>> task = Task<IList<string>>.Factory.StartNew(() =>
                {
                    var taskResponse = ApiClient.GetAsync<StickerResponse>(item);
                    var result = taskResponse.Result;
                    return result.stickers.Select(r => $"{item.Substring(0, item.LastIndexOf("/") + 1)}{r.sid}@1x.{r.ext}").ToList();
                });
                return task;
            }));
            return (from array in result from arr in array select arr).ToList();
        }

        public async Task<UploadResponse> UploadImage(IFormFile file)
        {
            var result = await ApiClient.PostAsync<UploadResponse>(ApiUrls.PathApiClientUploadImage, null, null, file);
            return result;
        }
    }
}