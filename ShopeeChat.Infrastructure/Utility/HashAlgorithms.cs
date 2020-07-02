using System.Security.Cryptography;
using System.Text;

namespace ShopeeChat.Infrastructure.Utility
{
    public static class HashAlgorithms
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string GetSha256(string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            byte[] message = Encoding.ASCII.GetBytes(text);
            SHA256Managed hashString = new SHA256Managed();
            byte[] hashValue = hashString.ComputeHash(message);
            string hashStringResult = string.Empty;
            foreach (byte x in hashValue)
            {
                hashStringResult += $"{x:x2}";
            }

            return hashStringResult;
        }
    }
}