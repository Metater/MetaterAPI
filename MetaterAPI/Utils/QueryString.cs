using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Interfaces.Server;
using System.Collections.Specialized;

namespace MetaterAPI.Utils
{
    public static class QueryString
    {
        public static string IfOneQueryStringAndMatch(IHttpContext context, string queryArg)
        {
            if ((context.Request.QueryString[queryArg] != null) && context.Request.QueryString.Count == 1)
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
        public static string GetMatchingQueryStringKeyFromNameList(IHttpContext context, string key, List<string> names)
        {
            foreach (string name in names)
                if (names.Contains(context.Request.QueryString[key]))
                    return names[names.IndexOf((context.Request.QueryString[key]))];
            return null;
        }
        public static bool ContainsQueryString(IHttpContext context, string key)
        {
            foreach(string queryStringKey in context.Request.QueryString.AllKeys)
                if (queryStringKey == key)
                    return true;
            return false;
        }

    }
}
