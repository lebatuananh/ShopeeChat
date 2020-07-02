namespace ShopeeChat.CoreAPI.Statics
{
    public static class Constants
    {
        /// <summary>
        /// Select object in request body
        /// </summary>
        public static int FormData = 1;
        public static int JsonBody = 2;
        public static int MultimediaBody = 3;

        /// <summary>
        /// Choose the type of chat message
        /// </summary>
        public static string TypeText = "text";
        public static string TypeImage = "image";
        public static string TypeSticker = "sticker";


        public static string TokenFake =
            "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjcmVhdGVfdGltZSI6MTU5MzMxMzExMiwiaWQiOiI0MDM2NDVhMy1iOGViLTExZWEtYTA0Yy1jY2JiZmUxNTU0YjIifQ.PpEkVyX43epILCbpI5Ojwh7SsRAL7hLnX6JFNNaoDFM";
    }
}