using Newtonsoft.Json;
using System;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ConversationDetailsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("shop_id")]
        public long ShopId { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("from_id")]
        public long FromId { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("from_shop_id")]
        public long FromShopId { get; set; }

        [JsonProperty("to_shop_id")]
        public long ToShopId { get; set; }

        [JsonProperty("from_user_name")]
        public string FromUserName { get; set; }

        [JsonProperty("to_user_name")]
        public string ToUserName { get; set; }

        [JsonProperty("seller_uid")]
        public long SellerUid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        //[JsonProperty("source_content")]
        //public string SourceContent { get; set; }

        [JsonProperty("created_timestamp")]
        public long CreatedTimestamp { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("IsAutoReply")]
        public bool IsAutoReply { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message_option")]
        public long MessageOption { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class Content
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
