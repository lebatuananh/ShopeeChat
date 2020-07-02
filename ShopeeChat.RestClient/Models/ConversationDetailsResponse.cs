using Newtonsoft.Json;
using System;

namespace ShopeeChat.RestClient.Models
{
    public class ConversationDetailsResponse
    {
        public string Id { get; set; }

        public long ShopId { get; set; }

        public string RequestId { get; set; }

        public long FromId { get; set; }

        public long ToId { get; set; }

        public long FromShopId { get; set; }

        public long ToShopId { get; set; }

        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public long SellerUid { get; set; }

        public string Type { get; set; }

        public Content Content { get; set; }

        public string ConversationId { get; set; }

        public string SourceType { get; set; }

        public object SourceContent { get; set; }

        public long CreatedTimestamp { get; set; }

        public string Country { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool IsAutoReply { get; set; }

        public string Status { get; set; }

        public long MessageOption { get; set; }

        public string Source { get; set; }
    }

    public class Content
    {
        public string Text { get; set; }

        public string StickerId { get; set; }

        public string StickerPackageId { get; set; }

        public string StickerUrl { get; set; }

        public string Url { get; set; }
    }
}