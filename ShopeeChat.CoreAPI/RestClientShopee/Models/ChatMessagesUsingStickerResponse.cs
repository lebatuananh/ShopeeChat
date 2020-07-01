using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ChatMessagesUsingStickerResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("request_id")]
        public Guid RequestId { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public ContentChatMessagesUsingStickerResponse Content { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("created_timestamp")]
        public long CreatedTimestamp { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("source_content")]
        public SourceContent SourceContent { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("message_option")]
        public long MessageOption { get; set; }
    }

    public class ContentChatMessagesUsingStickerResponse
    {
        [JsonProperty("sticker_id")]
        public string StickerId { get; set; }

        [JsonProperty("sticker_package_id")]
        public string StickerPackageId { get; set; }

        [JsonProperty("product_ids")]
        public object ProductIds { get; set; }
    }
}