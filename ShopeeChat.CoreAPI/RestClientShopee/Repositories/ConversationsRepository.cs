using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using ShopeeChat.CoreAPI.Statics;
using ShopeeChat.Infrastructure.Extensions;
using ShopeeChat.Infrastructure.Utility;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories
{
    public class ConversationsRepository : BaseRepository, IConversationsRepository
    {
        public ConversationsRepository(ShopeeApiClient shopeeApiClient) : base(shopeeApiClient)
        {
        }

        public async Task<UnreadCountResponse> GetUnreadCount()
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value = Constants.TokenFake
                }
            };
            var result =
                await _shopeeApiClient.GetAsync<UnreadCountResponse>(ApiUrls.PathUnreadCountConversations,
                    headerModels);
            return result;
        }

        public async Task<ResultConversationSyncResponse> SyncConversations()
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value = Constants.TokenFake                }
            };
            var result =
                await _shopeeApiClient.PostAsync<ResultConversationSyncResponse>(ApiUrls.PathSyncConversations, null, Constants.FormData,
                    headerModels);
            return result;
        }

        public async Task<List<ConversationResponse>> GetAllConversation()
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value = Constants.TokenFake                }
            };
            var result =
                await _shopeeApiClient.GetAsync<List<ConversationResponse>>(ApiUrls.PathGetAllConversations, headerModels);
            return result;
        }

        public async Task<List<ConversationDetailsResponse>> GetDetailConversation(string conversationId)
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value =
                        Constants.TokenFake                }
            };
            var result =
                await _shopeeApiClient.GetAsync<List<ConversationDetailsResponse>>(
                    string.Format(ApiUrls.PathGetDetailConversations, conversationId), headerModels);
            return result;
        }

        public async Task<object> SendChatMessage(ChatMessageRequest chatMessageRequest)
        {
            var result = new object();
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value =
                        Constants.TokenFake
                }
            };
            if (chatMessageRequest.Type == Constants.TypeText)
            {
                var chatMessagesUsingTextRequest = new ChatMessagesUsingTextRequest()
                {
                    RequestId = chatMessageRequest.RequestId,
                    Content = new ContentChatMessagesUsingTextRequest() { Text = chatMessageRequest.Content.Text },
                    Type = chatMessageRequest.Type,
                    ChatSendOption = chatMessageRequest.ChatSendOption,
                    Source = chatMessageRequest.Source,
                    ToId = chatMessageRequest.ToId
                };
                result = await _shopeeApiClient.PostAsync<ChatMessagesUsingTextResponse>(ApiUrls.PathSendChatMessage,
                   chatMessagesUsingTextRequest, Constants.JsonBody, headerModels); ;
                return result;

            }

            if (chatMessageRequest.Type == Constants.TypeSticker)
            {
                var chatMessagesUsingStickerRequest = new ChatMessagesUsingStickerRequest()
                {
                    RequestId = chatMessageRequest.RequestId,
                    Type = chatMessageRequest.Type,
                    ChatSendOption = chatMessageRequest.ChatSendOption,
                    Source = chatMessageRequest.Source,
                    ToId = chatMessageRequest.ToId,
                    Content = new ContentMessagesUsingStickerRequest() { StickerId = chatMessageRequest.Content.StickerId, StickerPackageId = chatMessageRequest.Content.StickerPackageId }
                };
                result = await _shopeeApiClient.PostAsync<ChatMessagesUsingTextResponse>(ApiUrls.PathSendChatMessage,
                    chatMessagesUsingStickerRequest, Constants.JsonBody, headerModels); ;
                return result;
            }

            if (chatMessageRequest.Type == Constants.TypeImage)
            {
                var chatMessagesUsingImageRequest = new ChatMessagesUsingImageRequest()
                {
                    RequestId = chatMessageRequest.RequestId,
                    Type = chatMessageRequest.Type,
                    ChatSendOption = chatMessageRequest.ChatSendOption,
                    Source = chatMessageRequest.Source,
                    ToId = chatMessageRequest.ToId,
                    Content = new ContentChatMessagesUsingImageRequest() { Url = chatMessageRequest.Content.Url }
                };
                result = await _shopeeApiClient.PostAsync<ChatMessagesUsingTextResponse>(ApiUrls.PathSendChatMessage,
                    chatMessagesUsingImageRequest, Constants.JsonBody, headerModels); ;
                return result;
            }

            return result;
        }

        public async Task<List<NewMessageResponse>> GetNewMessage()
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value =
                        Constants.TokenFake
                }
            };
            var obj = new
            {
                next_timestamp_nano = DateTime.Now.ToNanoseconds(),
                //next_timestamp_nano = "1593036320000000000",
                direction = "latest"
            };
            var result = await _shopeeApiClient.GetAsync<List<NewMessageResponse>>(ApiUrls.PathGetAllConversations,
                headerModels, obj.ToDictionary());
            return result;
        }
    }
}