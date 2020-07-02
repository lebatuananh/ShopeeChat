using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopeeChat.CoreAPI.Exceptions;
using ShopeeChat.CoreAPI.Statics;

namespace ShopeeChat.CoreAPI.Middlewares
{
    public class DetectClientAppMiddleware : BaseCustomMiddleware
    {
        public DetectClientAppMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
                await next(context);
            else
            {
                var clientAppName = ApiStaticVariables.ClientAppDetector.GetCurrentClientAppName(context.Request);
                if (string.IsNullOrWhiteSpace(clientAppName))
                    throw new BadRequestException(string.Format(ApiMessages.MissingOrInvalidAppTokenHeader,
                        ApiStaticVariables.ClientAppDetector.HeaderName));
                await next(context);
            }
        }
    }
}