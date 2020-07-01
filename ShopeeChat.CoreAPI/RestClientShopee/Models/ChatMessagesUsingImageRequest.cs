using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ChatMessagesUsingImageRequest
    {
        [JsonProperty("request_id")]
        public Guid RequestId { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public ContentChatMessagesUsingImageRequest Content { get; set; }

        [JsonProperty("chat_send_option")]
        public ChatSendOption ChatSendOption { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class ContentChatMessagesUsingImageRequest
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}