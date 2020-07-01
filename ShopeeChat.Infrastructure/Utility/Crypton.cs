using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DVG.AutoPortal.InfrastructureLayer.Utility
{
    public sealed class CryptorEngine
    {
        // Fields
        private const string INTERNAL_KEY = "sdt-21-02-2010-@#$111111";

        // Methods
        public CryptorEngine() { }

        public static string Decrypt(string cipherString)
        {
            return Decrypt(cipherString, true, INTERNAL_KEY);
        }

        public static string Decrypt(string cipherString, string key)
        {
            return Decrypt(cipherString, true, key);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            return Decrypt(cipherString, useHashing, INTERNAL_KEY);
        }

        public static string Decrypt(string cipherString, bool useHashing, string key)
        {
            byte[] buffer;
            //byte[] inputBuffer = HttpServerUtility.UrlTokenDecode(cipherString);
            byte[] inputBuffer = Convert.FromBase64String(cipherString);
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                provider.Clear();
            }
            else
            {/**/
                buffer = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            provider2.Clear();
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, true, INTERNAL_KEY);
        }

        public static string Encrypt(string toEncrypt, string publishKey)
        {
            return Encrypt(toEncrypt, true, publishKey);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            return Encrypt(toEncrypt, useHashing, INTERNAL_KEY);
        }

        public static string Encrypt(string toEncrypt, bool useHashing, string key)
        {
            byte[] buffer;
            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] input = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            provider2.Clear();
            return Convert.ToBase64String(input);
        }
    }

    public class CryptoEngine
    {
        private static readonly byte[] IV = new byte[] { 240, 3, 0x2d, 0x1d, 0, 0x4c, 0xad, 0x3b };
        private static readonly TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
        public static string Decrypt(string encryptedQueryString, string publishKey)
        {
            string str = string.Empty;
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(encryptedQueryString);
                provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(publishKey));
                provider.IV = IV;
                str = Encoding.UTF8.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (CryptographicException cEx)
            {
            }
            catch (FormatException fEx)
            {
            }
            return str;
        }

        public static string Encrypt(string serializedQueryString, string publishKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(serializedQueryString);
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(publishKey));
            provider.IV = IV;
            return Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string MD5Encrypt(string plainText)
        {
            byte[] data, output;
            var encoder = new UTF8Encoding();
            var hasher = new MD5CryptoServiceProvider();

            data = encoder.GetBytes(plainText);
            output = hasher.ComputeHash(data);

            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        public static string Md5x2(string str)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = provider.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in bytes)
            {
                builder.Append(num.ToString("x2").ToLower());
            }
            return builder.ToString();
        }
    }

    public class Crypton
    {
        private static string hashAlgorithm = "SHA1";
        private static string initVector = "@1B2c3D4e5F6g7H8";
        private static int keySize = 0x100;
        private static string passPhrase = "Pas5pr@se";
        private static int passwordIterations = 2;
        private static string saltValue = "s@1tValue";
        public static string Decrypt(string cipherText)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(initVector);
                byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
                byte[] buffer = null;
                try
                {
                    buffer = Convert.FromBase64String(cipherText);
                }
                catch
                {
                    return string.Empty;
                }
                byte[] rgbKey =
                    new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
                ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(rgbKey, bytes);

                byte[] buffer5;
                int count;
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        buffer5 = new byte[buffer.Length];
                        count = stream2.Read(buffer5, 0, buffer5.Length);
                        stream2.Close();
                    }
                    stream.Close();
                }

                return Encoding.UTF8.GetString(buffer5, 0, count);
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }

        public static string DecryptByDay(string cipherText)
        {
            try
            {
                string strPassword = DateTime.Today.ToString("ddMMyyyy");
                string s = DateTime.Today.ToString("yyyyMMdd");
                byte[] bytes = Encoding.ASCII.GetBytes(initVector);
                byte[] rgbSalt = Encoding.ASCII.GetBytes(s);
                byte[] buffer = Convert.FromBase64String(cipherText);
                byte[] rgbKey =
                    new PasswordDeriveBytes(strPassword, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
                ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(rgbKey, bytes);

                byte[] buffer5;
                int count;

                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        buffer5 = new byte[buffer.Length];
                        count = stream2.Read(buffer5, 0, buffer5.Length);
                        stream2.Close();
                    }
                    stream.Close();
                }
                return Encoding.UTF8.GetString(buffer5, 0, count);
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }

        public static string DecryptByKey(string cipherText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                {
                    return string.Empty;
                }
                string strPassword = key + "passPhrase";
                string s = key + "saltValue";
                byte[] bytes = Encoding.ASCII.GetBytes(initVector);
                byte[] rgbSalt = Encoding.ASCII.GetBytes(s);
                byte[] buffer = Convert.FromBase64String(cipherText);
                byte[] rgbKey =
                    new PasswordDeriveBytes(strPassword, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
                ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(rgbKey, bytes);
                byte[] buffer5;
                int count;

                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        buffer5 = new byte[buffer.Length];
                        count = stream2.Read(buffer5, 0, buffer5.Length);
                        stream2.Close();
                    }
                    stream.Close();
                }
                return Encoding.UTF8.GetString(buffer5, 0, count);
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            byte[] rgbKey =
                new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
            ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateEncryptor(rgbKey, bytes);

            byte[] inArray;

            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    inArray = stream.ToArray();
                    stream2.Close();
                }
                stream.Close();
            }

            return Convert.ToBase64String(inArray);
        }

        public static string EncryptByDay(string plainText)
        {
            string strPassword = DateTime.Today.ToString("ddMMyyyy");
            string s = DateTime.Today.ToString("yyyyMMdd");
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(s);
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            byte[] rgbKey = new PasswordDeriveBytes(strPassword, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
            ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateEncryptor(rgbKey, bytes);

            byte[] inArray;

            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    inArray = stream.ToArray();
                    stream2.Close();
                }
                stream.Close();
            }

            return Convert.ToBase64String(inArray);
        }

        public static string EncryptByKey(string plainText, string key)
        {
            try
            {
                string strPassword = key + "passPhrase";
                string s = key + "saltValue";
                byte[] bytes = Encoding.ASCII.GetBytes(initVector);
                byte[] rgbSalt = Encoding.ASCII.GetBytes(s);
                byte[] buffer = Encoding.UTF8.GetBytes(plainText);
                byte[] rgbKey =
                    new PasswordDeriveBytes(strPassword, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
                ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateEncryptor(rgbKey, bytes);
                byte[] inArray;

                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                        inArray = stream.ToArray();
                        stream2.Close();
                    }
                    stream.Close();
                }

                return Convert.ToBase64String(inArray);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private const string rpl1 = "=";
        private const string rpl11 = "_______";
        private const string rpl2 = "+";
        private const string rpl21 = "@@@";
        private const string rpl3 = "/";
        private const string rpl31 = "$$$";

        public static string EncryptForHTML(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            string output = Encrypt(plainText);
            return output.Replace(rpl1, rpl11).Replace(rpl2, rpl21).Replace(rpl3, rpl31);
        }

        public static string DecryptFromHTML(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;
            cipherText = cipherText.Replace(rpl11, rpl1).Replace(rpl21, rpl2).Replace(rpl31, rpl3);
            return Decrypt(cipherText);
        }

        public static string EncryptForHTMLByKey(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            string output = EncryptByKey(plainText, key);
            return output.Replace(rpl1, rpl11).Replace(rpl2, rpl21).Replace(rpl3, rpl31);
        }

        public static string DecryptFromHTMLByKey(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;
            cipherText = cipherText.Replace(rpl11, rpl1).Replace(rpl21, rpl2).Replace(rpl31, rpl3);
            return DecryptByKey(cipherText, key);
        }
    }
}