using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopeeChat.CoreAPI.Exceptions;
using ShopeeChat.CoreAPI.Models;

namespace ShopeeChat.CoreAPI.Filters
{
    public class CustomApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is BaseCustomException customException)
            {
                context.HttpContext.Response.StatusCode = (int) customException.StatusCode;
                var errorModel = new ErrorModel(customException.StatusCode, customException.Messages);
                if (customException.AdditionalData != null)
                    errorModel.AdditionalData = customException.AdditionalData;
                context.Result = new JsonResult(errorModel);
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(
                    new ErrorModel(
                        HttpStatusCode.InternalServerError,
                        new List<string>()
                        {
                            string.Format("Có lỗi xảy ra ({0})", exception.Message ?? "")
                        }
                    )
                );
            }

            base.OnException(context);
        }
    }
}