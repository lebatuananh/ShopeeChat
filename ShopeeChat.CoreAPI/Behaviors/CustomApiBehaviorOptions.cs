using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.Models;

namespace ShopeeChat.CoreAPI.Behaviors
{
    public class CustomApiBehaviorOptions
    {
        public static Func<ActionContext, IActionResult> InvalidModelStateResponseFactory = (actionContext) =>
        {
            var errors = actionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .Select(e => string.Format("{0}: {1}", e.Key, e.Value.Errors.First().ErrorMessage))
                .ToList();
            var errorModel = new ErrorModel(System.Net.HttpStatusCode.BadRequest, errors);
            return new BadRequestObjectResult(errorModel);
        };
    }
}