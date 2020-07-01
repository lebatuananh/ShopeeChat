using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ChatMessageRequest
    {
        [JsonProperty("request_id")]
        public Guid RequestId { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public ContentChatMessagesRequest Content { get; set; }

        [JsonProperty("chat_send_option")]
        public ChatSendOption ChatSendOption { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class ContentChatMessagesRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("sticker_id")]
        public string StickerId { get; set; }

        [JsonProperty("sticker_package_id")]
        public string StickerPackageId { get; set; }
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}