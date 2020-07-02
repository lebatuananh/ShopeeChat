﻿using System;
using System.Collections.Generic;
using System.Text;
 using ShopeeChat.CoreAPI.Exceptions;

 namespace DVG.AutoPortal.Indian.Satellite.API.Core.Exceptions
{
    public class ForbiddenException : BaseCustomException
    {
        public ForbiddenException(string message) : base(new List<string>() { message }, System.Net.HttpStatusCode.Forbidden)
        {

        }
    }
}
