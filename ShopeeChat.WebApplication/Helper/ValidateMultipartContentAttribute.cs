using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopeeChat.WebApplication.Helper
{
    public class ValidateMultipartContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.IsMultipartContentType())
            {
                context.Result = new StatusCodeResult(StatusCodes.Status415UnsupportedMediaType);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
