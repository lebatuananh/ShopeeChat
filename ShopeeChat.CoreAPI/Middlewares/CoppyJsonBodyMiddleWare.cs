﻿using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
 using ShopeeChat.CoreAPI.Middlewares;

 namespace DVG.AutoPortal.Indian.API.Application.Middlewares
{
    public class CoppyJsonBodyMiddleware : BaseCustomMiddleware
    {
        public CoppyJsonBodyMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Method == HttpMethods.Get)
                await next(context);
            else
            {
                using (var bodyCopierStream = new MemoryStream())
                {
                    await request.Body.CopyToAsync(bodyCopierStream);

                    bodyCopierStream.Position = 0;

                    var streamReader = new StreamReader(bodyCopierStream);
                    string bodyText = await streamReader.ReadToEndAsync();
                    context.Items.Add("BodyText", bodyText);

                    bodyCopierStream.Position = 0;
                    request.Body = bodyCopierStream;
                    await next(context);
                }
            }
        }
    }
}