using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.Statics;
using ShopeeChat.Infrastructure.Utility;

namespace ShopeeChat.CoreAPI.RestClientShopee
{
    public abstract class CustomHttpClient : HttpClient
    {
        public abstract T Deserialize<T>(string json);
        public abstract string Serialize(object obj);

        public virtual async Task PreprocessResponse(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                string bodyText = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception(bodyText);
            }
        }

        public virtual HttpRequestMessage InitRequest(HttpMethod method, string path,
            Dictionary<string, string> queries)
        {
            string requestUri = path;
            if (queries != null)
            {
                var listQuery = queries.Where(pair => pair.Value != null)
                    .Select(s => string.Format("{0}={1}", s.Key, s.Value));
                var queryString = string.Join("&", listQuery);
                requestUri = string.Format("{0}?{1}", requestUri, queryString);
            }

            HttpRequestMessage request = new HttpRequestMessage(method, requestUri);
            return request;
        }

        public virtual void SetRequestBody(HttpRequestMessage requestMessage, object obj, int option)
        {
            if (obj == null) return;
            if (option == Constants.FormData)
            {
                var formData = obj.ToDictionary();
                requestMessage.Content = new FormUrlEncodedContent(formData);
            }
            if (option == Constants.JsonBody)
            {
                requestMessage.Content = new StringContent(Serialize(obj), Encoding.UTF8, "application/json");
            }

            if (option == Constants.MultimediaBody)
            {
                byte[] data;
                var file = (IFormFile)obj;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestMessage.Content = new MultipartFormDataContent { { bytes, "file", file.FileName } };
            }

        }

        public virtual void SetHeader(HttpRequestMessage requestMessage, List<HeaderModel> headerModels)
        {
            if (headerModels != null)
            {
                foreach (var item in headerModels)
                {
                    if (!requestMessage.Headers.Contains(item.Key))
                    {
                        requestMessage.Headers.Add(item.Key, item.Value);
                    }
                }
            }

        }

        public async Task<T> GetAsync<T>(string path, List<HeaderModel> headerModels = null, Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Get, path, queries);
            SetHeader(requestMessage, headerModels);
            var responseMessage = await SendAsync(requestMessage);
            await PreprocessResponse(responseMessage);
            string bodyText = await responseMessage.Content.ReadAsStringAsync();
            return Deserialize<T>(bodyText);
        }

        public async Task<byte[]> GetStreamAsync<T>(string path, List<HeaderModel> headerModels = null, Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Get, path, queries);
            SetHeader(requestMessage, headerModels);
            var responseMessage = await SendAsync(requestMessage);
            await PreprocessResponse(responseMessage);
            byte[] content = await responseMessage.Content.ReadAsByteArrayAsync();
            return content;
        }

        public async Task<T> PostAsync<T>(string path, object obj, int option, List<HeaderModel> headerModels = null,
            Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Post, path, queries);
            SetRequestBody(requestMessage, obj, option);
            SetHeader(requestMessage, headerModels);
            var responseMessage = await SendAsync(requestMessage);
            await PreprocessResponse(responseMessage);
            string bodyText = await responseMessage.Content.ReadAsStringAsync();
            return Deserialize<T>(bodyText);
        }


        //public async Task<T> PostRefreshTokenAsync<T>(string path, object obj,
        //    Dictionary<string, string> queries = null)
        //{
        //    var requestMessage = InitRequest(HttpMethod.Post, path, queries);
        //    requestMessage.Headers.Add("cookie",
        //        "SPC_EC=2qEEwzLbbyAZ/iYk5Hu7Gqj/E3/SAb5xFp2Ih7FfXyhzurlqN5/kNWxRYifQKBE/WsTRplqwVJkUEN9UrmCdxp3e9CbwWnp9hKGpABU6Ze3VR3n7xoSUEe9OA0y7/Hf9pUBsoywaQRnoHpaRFPhHng==");
        //    SetRequestBody(requestMessage, obj);
        //    var responseMessage = await SendAsync(requestMessage);
        //    await PreprocessResponse(responseMessage);
        //    string bodyText = await responseMessage.Content.ReadAsStringAsync();
        //    return Deserialize<T>(bodyText);
        //}

        public async Task<T> PostLoginShopeeAsync<T>(string path, object obj, int option, Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Post, path, queries);
            SetRequestBody(requestMessage, obj, option);
            var responseMessage = await SendAsync(requestMessage);
            var resultCookie = string.Empty;
            foreach (var item in responseMessage.Headers)
            {
                if (item.Key == "Set-Cookie")
                {
                    foreach (var itemj in item.Value)
                    {
                        if (itemj.StartsWith("SPC_F"))
                        {
                            var listValues = itemj.Split(";");
                            resultCookie = listValues[0];
                        }
                        if (itemj.StartsWith("SPC_EC"))
                        {
                            var listValues = itemj.Split(";");
                            var keyCookie = listValues[0].Split("=")[0];
                            var valueCookie = listValues[0].Split("=")[1];
                            resultCookie = $"\"{keyCookie}\":{valueCookie}==\"";
                        }
                    }
                }
            }
            await PreprocessResponse(responseMessage);
            string bodyText = await responseMessage.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(resultCookie))
            {
                bodyText = bodyText.Insert(bodyText.Length - 1, "," + resultCookie);
            }

            return Deserialize<T>(bodyText);
        }

        public async Task<T> PutAsync<T>(string path, object obj, int option, Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Put, path, queries);
            SetRequestBody(requestMessage, obj, option);
            var responseMessage = await SendAsync(requestMessage);
            await PreprocessResponse(responseMessage);
            string bodyText = await responseMessage.Content.ReadAsStringAsync();
            return Deserialize<T>(bodyText);
        }

        public async Task<T> DeleteAsync<T>(string path, object obj, int option, Dictionary<string, string> queries = null)
        {
            var requestMessage = InitRequest(HttpMethod.Delete, path, queries);
            SetRequestBody(requestMessage, obj, option);
            var responseMessage = await SendAsync(requestMessage);
            await PreprocessResponse(responseMessage);
            string bodyText = await responseMessage.Content.ReadAsStringAsync();
            return Deserialize<T>(bodyText);
        }
    }
}