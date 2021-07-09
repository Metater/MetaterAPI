using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System;
using System.Text;

namespace MetaterAPI
{
    [RestResource]
    public class WeatherStation
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/weather")]
        public IHttpContext Test(IHttpContext context)
        {
            Console.WriteLine("EEAWDSADASDSA");
            context.Response.SendResponse("How is the weather up there?");
            context.Response.StatusCode = HttpStatusCode.Ok;
            return context;
        }
    }
}
