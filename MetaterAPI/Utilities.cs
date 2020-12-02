﻿using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace MetaterAPI
{
    public static class Utilities
    {
        public static string IfOneQueryStringAndMatch(IHttpContext context, string queryArg)
        {
            if ((context.Request.QueryString[queryArg] != null) && context.Request.QueryString.Count == 0)
            {
                return context.Request.QueryString[queryArg];
            }
            return null;
        }
        public static string IfQueryStringMatch(IHttpContext context, string queryArg)
        {
            if ((context.Request.QueryString[queryArg] != null) && context.Request.QueryString.Count > 0)
            {
                return context.Request.QueryString[queryArg];
            }
            return null;
        }
        public static string IfQueryStringMatchAndNoOtherQueryString(IHttpContext context, string queryArg, string otherQueryArg)
        {
            if ((context.Request.QueryString[queryArg] != null) && (context.Request.QueryString[otherQueryArg] == null))
            {
                return context.Request.QueryString[queryArg];
            }
            return null;
        }
        public static string IfNoQueryArgs(IHttpContext context)
        {
            if (context.Request.QueryString.Count != 0)
                return "NoQueryArgs";
            return null;
        }
    }
}
