using Newtonsoft.Json.Serialization;

namespace Route.API
{
    public class GremlinSettings
    {
        public static string hostname = "usroutes.gremlin.cosmos.azure.com";
        public static int port = 443;
        public static string authKey = "TLERYrRtR7JpvhaaAN9iB34niW7n7F5gy9E7kw5eG9L8tlSWj2raOnb24LQsX0fH7RnfM5ZiQ8cvzHNS0EISUw==";
        public static string database = "RoutesDB";
        public static string collection = "Neighbours";


    }

}
