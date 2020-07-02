﻿using System.Collections.Generic;
 using ShopeeChat.CoreAPI.Exceptions;

 namespace DVG.AutoPortal.Indian.Satellite.API.Core.Exceptions
{
    public class UnprocessableEntityException : BaseCustomException
    {
        public UnprocessableEntityException(List<string> errors) : base(errors, System.Net.HttpStatusCode.UnprocessableEntity)
        {
        }

        public UnprocessableEntityException(string error) : base(new List<string>() { error }, System.Net.HttpStatusCode.UnprocessableEntity)
        {
        }
    }
}