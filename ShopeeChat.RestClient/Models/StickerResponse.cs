using System.Collections.Generic;

namespace ShopeeChat.RestClient.Models
{
    public class StickerResponse
    {
        public bool auto_download { get; set; }
        public string[] locales { get; set; }
        public string[] size { get; set; }
        public IList<Stickers> stickers { get; set; }
    }

    public class Stickers
    {
        public string ext { get; set; }
        public string[] name { get; set; }
        public string sid { get; set; }
    }
}
